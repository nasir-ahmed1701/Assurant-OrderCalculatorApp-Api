using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Business.IServices
{
    public interface IOrderTaxService
    {
        Task<(decimal, decimal, decimal)> ComputerOrderTaxAsync(int orderId, string clientCode, CancellationToken cancellationToken = default);
    }
}
