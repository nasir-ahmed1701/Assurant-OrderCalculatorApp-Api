using OrderCalculatorApp.Data.Interfaces;
using OrderCalculatorApp.Shared;
using OrderCalculatorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Data.Repositories
{
    /// <summary>
    /// Assumption : Getting all the information from a DB , so considering at asyn tasks
    /// </summary>
    public class PromotionRepository : IPromotionRepository
    {
        private readonly List<Promotion> _promotions = GetMockPromotions();

        private static List<Promotion> GetMockPromotions()
        {
            return [
                    new Promotion{
                        Id = 1,
                        Code = Constants.PromotionCodes.PROMOTION_5,
                        DiscountPercentage = 5,
                        FromDate = new DateTime(2024,8,1),
                        ToDate = new DateTime(2024,8,10)
                    },
                    new Promotion{
                        Id = 2,
                        Code = Constants.PromotionCodes.PROMOTION_10,
                        DiscountPercentage = 10,
                        FromDate = new DateTime(2024,8,1),
                        ToDate = new DateTime(2024,8,5)
                    }
                ];
        }

        public PromotionRepository()
        {

        }

        public async Task<decimal?> GetDiscountPercentageAsync(string code, DateTime dateTime)
        {
            return await Task.Run(() =>
            {
                return _promotions.FirstOrDefault(x => x.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)
                && x.FromDate <= dateTime && x.ToDate >= dateTime)?.DiscountPercentage;
            }).ConfigureAwait(false);
        }
    }
}
