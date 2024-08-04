using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Shared.Models
{
    public class OrderTaxCalculateResponse
    {
        public decimal TotalOrderPriceWithTax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalOrderPriceWithOutTax { get; set; }
    }
}
