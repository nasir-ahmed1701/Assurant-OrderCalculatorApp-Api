using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Shared.Models
{
    public class OrderTaxCalculateRequest
    {
        public int OrderId { get; set; }
        public required string ClientCode { get; set; }
    }
}
