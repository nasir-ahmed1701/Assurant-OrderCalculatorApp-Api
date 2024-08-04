using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorApp.Shared
{
    public static class Constants
    {
        public static readonly List<string> AcceptedClientCodes = [ClientCodes.GA, ClientCodes.FL, ClientCodes.NY, ClientCodes.NM, ClientCodes.NV];
        public static class ClientCodes
        {
            public const string GA = "GA";
            public const string FL = "FL";
            public const string NY = "NY";
            public const string NM = "NM";
            public const string NV = "NV";
        }

        public static class PromotionCodes
        {
            public const string PROMOTION_5 = "PROMOTION_5";
            public const string PROMOTION_10 = "PROMOTION_10";
        }

        public static class CouponCodes
        {
            public const string COUPON_5 = "COUPON_5";
            public const string COUPON_10 = "COUPON_10";
        }
    }
}
