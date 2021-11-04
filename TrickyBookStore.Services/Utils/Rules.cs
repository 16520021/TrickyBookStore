using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickyBookStore.Services.Utils
{
    public class Rules
    {
        public const string SUBSCRIPTION_FIX_PRICE_PROP = "FixPrice";
        public static class PremiumRules
        {
            public const int BOOK_QUANTITY_APPLY_DISCOUNT_PREMIUM = 3;
            public const double NEW_BOOK_PRICE_PREFIX = 0.75;
            public const double SUBSCRIPTION_FIX_PRICE = 200;
        }

        public static class PaidRules
        {
            public const int BOOK_QUANTITY_APPLY_DISCOUNT_PAID = 3;
            public const double NEW_BOOK_PRICE_PREFIX = 0.95;
            public const double OLD_BOOK_PRICE_PREFIX = 0.5;
            public const double SUBSCRIPTION_FIX_PRICE = 50;
        }

        public static class CategoryAddictedRules
        {
            public const int BOOK_QUANTITY_APPLY_DISCOUNT_CATEGORY = 3;
            public const double NEW_BOOK_PRICE_PREFIX = 0.75;
            public const double SUBSCRIPTION_FIX_PRICE = 75;
        }

        public static class FreeRules
        {
            public const double OLD_BOOK_PRICE_PREFIX = 0.9;
            public const double SUBSCRIPTION_FIX_PRICE = 0;
        }
    }
}