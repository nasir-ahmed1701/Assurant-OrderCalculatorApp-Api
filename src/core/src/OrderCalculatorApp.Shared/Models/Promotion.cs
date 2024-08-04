using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Shared.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
