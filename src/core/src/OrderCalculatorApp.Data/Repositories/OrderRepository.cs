using OrderCalculatorApp.Data.Interfaces;
using OrderCalculatorApp.Shared;
using OrderCalculatorApp.Shared.Models;

namespace OrderCalculatorApp.Data.Repositories
{
    /// <summary>
    /// Assumption : Getting all the information from a DB , so considering at asyn tasks
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = GetMockOrders();

        private static List<Order> GetMockOrders()
        {
            return [GetGAOrder1(), GetFLOrder1(), GetNYOrder1(), GetNMOrder1(), GetNVOrder1()];
        }

        public OrderRepository()
        {

        }

        public async Task<Order?> GetOrderAsync(int id, string clientCode)
        {
            return await Task.Run(() =>
            {
                return _orders.FirstOrDefault(x => x.Id == id
                && x.ClientCode.Equals(clientCode, StringComparison.InvariantCultureIgnoreCase));
            }).ConfigureAwait(false);
        }

        private static Order GetGAOrder1()
        {
            return
                new Order
                {
                    Id = 1,
                    ClientCode = Constants.ClientCodes.GA,
                    CreatedDate = new DateTime(2024, 08, 1),
                    CustomerId = 1,
                    PromotionCode = "PROMOTION_5",
                    Products = [
                        new Product{
                            Id = 1,
                            Name="Shirt",
                            CouponCode = "COUPON_5",
                            Price = 1000
                        },
                        new Product{
                            Id = 2,
                            Name="Pant",
                            CouponCode = null,
                            Price = 2000
                        }
                        ]
                };
        }


        private static Order GetFLOrder1()
        {
            return
                new Order
                {
                    Id = 2,
                    ClientCode = Constants.ClientCodes.FL,
                    CreatedDate = new DateTime(2024, 08, 2),
                    CustomerId = 1,
                    PromotionCode = "PROMOTION_10",
                    Products = [
                        new Product{
                            Id = 1,
                            Name="Shirt",
                            CouponCode = "COUPON_10",
                            Price = 1000
                        },
                        new Product{
                            Id = 2,
                            Name="Pant",
                            CouponCode = null,
                            Price = 2000
                        }
                        ]
                };
        }

        private static Order GetNYOrder1()
        {
            return
                new Order
                {
                    Id = 2,
                    ClientCode = Constants.ClientCodes.NY,
                    CreatedDate = new DateTime(2024, 08, 5),
                    CustomerId = 1,
                    PromotionCode = "PROMOTION_10",
                    Products = [
                        new Product{
                            Id = 1,
                            Name="Shirt",
                            CouponCode = "COUPON_10",
                            Price = 1000
                        },
                        new Product{
                            Id = 2,
                            Name="Pant",
                            CouponCode = null,
                            Price = 2000
                        }
                        ]
                };
        }

        private static Order GetNMOrder1()
        {
            return
                new Order
                {
                    Id = 2,
                    ClientCode = Constants.ClientCodes.NM,
                    CreatedDate = new DateTime(2024, 08, 4),
                    CustomerId = 1,
                    PromotionCode = "PROMOTION_5",
                    Products = [
                        new Product{
                            Id = 1,
                            Name="Shirt",
                            CouponCode = "COUPON_5",
                            Price = 1000
                        },
                        new Product{
                            Id = 2,
                            Name="Pant",
                            CouponCode = null,
                            Price = 2000
                        }
                        ]
                };
        }

        private static Order GetNVOrder1()
        {
            return
                new Order
                {
                    Id = 2,
                    ClientCode = Constants.ClientCodes.NV,
                    CreatedDate = new DateTime(2024, 08, 7),
                    CustomerId = 1,
                    PromotionCode = "PROMOTION_10",
                    Products = [
                        new Product{
                            Id = 1,
                            Name="Shirt",
                            CouponCode = "COUPON_10",
                            Price = 1000
                        },
                        new Product{
                            Id = 2,
                            Name="Pant",
                            CouponCode = null,
                            Price = 2000
                        }
                        ]
                };
        }

    }
}
