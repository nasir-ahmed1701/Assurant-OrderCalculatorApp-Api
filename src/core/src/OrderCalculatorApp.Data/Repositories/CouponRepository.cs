using OrderCalculatorApp.Data.Interfaces;
using OrderCalculatorApp.Shared;
using OrderCalculatorApp.Shared.Models;

namespace OrderCalculatorApp.Data.Repositories
{
    /// <summary>
    /// Assumption : Getting all the information from a DB , so considering at asyn tasks
    /// </summary>
    public class CouponRepository : ICouponRepository
    {
        private readonly List<Coupon> _coupons = GetMockCoupons();

        public CouponRepository()
        {

        }

        public Coupon? GetCoupon(string code)
        {
            return _coupons.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }

        public async Task<decimal?> GetDiscountPercentageAsync(string code, DateTime dateTime)
        {
            return await Task.Run(() =>
            {

                return _coupons.FirstOrDefault(x => x.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)
                && x.FromDate <= dateTime && x.ToDate >= dateTime)?.DiscountPercntage;
            });
        }

        private static List<Coupon> GetMockCoupons()
        {
            return
                [
                    new Coupon {
                        Id = 1,
                        Code = Constants.CouponCodes.COUPON_5,
                        DiscountPercntage=5,
                        FromDate = new DateTime(2024,8,1),
                        ToDate = new DateTime(2024,8,3)
                    },
                    new Coupon {
                        Id = 2,
                        Code = Constants.CouponCodes.COUPON_10,
                        DiscountPercntage=10,
                        FromDate = new DateTime(2024,8,1),
                        ToDate = new DateTime(2024,8,5)
                    }
                ];
        }
    }
}
