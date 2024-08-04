using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderCalculatorApp.Business.IServices;
using OrderCalculatorApp.Shared.Models;
using static OrderCalculatorApp.Shared.Constants;

namespace OrderCalculatorApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderCalculatorController : ControllerBase
    {
        private readonly IOrderTaxService _orderTaxService;

        public OrderCalculatorController(IOrderTaxService orderTaxService)
        {
            _orderTaxService = orderTaxService;
        }

        [HttpPost]
        public async Task<ActionResult> CalculateOrderTax([FromBody] OrderTaxCalculateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                {
                    throw new ArgumentNullException(nameof(request));
                }

                if (request.OrderId <= 0)
                {
                    throw new ArgumentException("Invalid orderId");
                }

                if (string.IsNullOrWhiteSpace(request.ClientCode))
                {
                    throw new ArgumentNullException(nameof(request.ClientCode));
                }

                var result = await _orderTaxService.ComputerOrderTaxAsync(request.OrderId, request.ClientCode, cancellationToken);

                var response = new OrderTaxCalculateResponse()
                {
                    TotalOrderPriceWithTax = result.Item1,
                    TaxAmount = result.Item2,
                    TotalOrderPriceWithOutTax = result.Item3
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured");
            }

        }
    }
}
