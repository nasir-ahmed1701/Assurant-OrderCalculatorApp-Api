﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Data.Interfaces
{
    public interface IPromotionRepository
    {
        public Task<decimal?> GetDiscountPercentageAsync(string code, DateTime dateTime);
    }
}
