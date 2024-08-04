using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Shared.Models
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public decimal TaxPercentage { get; set; }
        public bool IsApplyTaxBeforeDiscount { get; set; }
    }
}
