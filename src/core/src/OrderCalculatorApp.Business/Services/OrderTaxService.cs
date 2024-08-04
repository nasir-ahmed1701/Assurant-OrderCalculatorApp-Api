using OrderCalculatorApp.Business.IServices;
using OrderCalculatorApp.Data.Interfaces;
using OrderCalculatorApp.Shared;
using OrderCalculatorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Business.Services
{
    public class OrderTaxService : IOrderTaxService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly ICouponRepository _couponRepository;

        public OrderTaxService(
            IClientRepository clientRepository,
            IOrderRepository orderRepository,
            IPromotionRepository promotionRepository,
            ICouponRepository couponRepository
            )
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
            _promotionRepository = promotionRepository;
            _couponRepository = couponRepository;
        }

        public async Task<(decimal, decimal, decimal)> ComputerOrderTaxAsync(int orderId, string clientCode, CancellationToken cancellationToken = default)
        {
            if (!Constants.AcceptedClientCodes.Contains(clientCode, StringComparer.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Invalid client code");
            }

            decimal totalOrderPriceWithTax = 0;
            decimal taxAmount = 0;
            decimal totalOrderPriceWithOutTax = 0;

            var client = await _clientRepository.GetClientAsync(clientCode).ConfigureAwait(false);

            if (client is null)
            {
                throw new ArgumentException("Client information not found");
            }

            var order = await _orderRepository.GetOrderAsync(orderId, clientCode).ConfigureAwait(false);

            if (order is null)
            {
                throw new ArgumentException("Order information not found");
            }

            decimal? promotionPercentage = null;
            if (!string.IsNullOrWhiteSpace(order.PromotionCode))
            {
                promotionPercentage = await _promotionRepository.GetDiscountPercentageAsync(order.PromotionCode, order.CreatedDate).ConfigureAwait(false);
            }

            if (client.IsApplyTaxBeforeDiscount)
            {
                var totalOrderPriceWithOutDiscount = order.Products.Sum(x => x.Price);

                taxAmount = GetTaxAmount(totalOrderPriceWithOutDiscount, client.TaxPercentage);

                totalOrderPriceWithOutTax = await GetTotalOrderPriceAsync(order).ConfigureAwait(false);

                if (promotionPercentage is not null)
                {
                    totalOrderPriceWithOutTax = GetOrderTotalAfterPromotionDiscount(promotionPercentage.Value, totalOrderPriceWithOutTax);
                }

                totalOrderPriceWithTax = totalOrderPriceWithOutTax + taxAmount;
            }
            else
            {
                totalOrderPriceWithOutTax = await GetTotalOrderPriceAsync(order).ConfigureAwait(false);

                if (promotionPercentage is not null)
                {
                    totalOrderPriceWithOutTax = GetOrderTotalAfterPromotionDiscount(promotionPercentage.Value, totalOrderPriceWithOutTax);
                }

                taxAmount = GetTaxAmount(totalOrderPriceWithOutTax, client.TaxPercentage);

                totalOrderPriceWithTax = totalOrderPriceWithOutTax + taxAmount;
            }

            // logic to save totalOrderPriceWithTax,taxAmount,totalOrderPriceWithOutTax

            return (totalOrderPriceWithTax, taxAmount, totalOrderPriceWithOutTax);

        }

        private static decimal GetOrderTotalAfterPromotionDiscount(decimal promotionPercentage, decimal totalOrderPriceWithOutTax)
        {
            var discountAmount = (totalOrderPriceWithOutTax * promotionPercentage) / 100;
            totalOrderPriceWithOutTax -= discountAmount;
            return totalOrderPriceWithOutTax;
        }

        private static decimal GetTaxAmount(decimal orderTotal, decimal taxPercentage)
        {
            return (orderTotal * taxPercentage) / 100;
        }

        private async Task<decimal> GetTotalOrderPriceAsync(Order order)
        {
            decimal total = 0;
            foreach (var product in order.Products)
            {
                var productPrice = await GetProductPriceAfterDiscountAsync(product, order.CreatedDate).ConfigureAwait(false);
                total += productPrice;
            }
            return total;
        }


        private async Task<decimal> GetProductPriceAfterDiscountAsync(Product product, DateTime dateTime)
        {
            if (!string.IsNullOrWhiteSpace(product.CouponCode))
            {
                var dicountPercentage = await _couponRepository.GetDiscountPercentageAsync(product.CouponCode!, dateTime).ConfigureAwait(false);

                var discountAmount = (product.Price * dicountPercentage) / 100;

                return (product.Price - (decimal)discountAmount);
            }
            return product.Price;
        }
    }
}
