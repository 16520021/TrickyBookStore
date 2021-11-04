using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Utils;

namespace TrickyBookStore.Services.Store
{
    public static class Subscriptions
    {
        public static readonly IEnumerable<Subscription> Data = new List<Subscription>
        {
            new Subscription { Id = 1, SubscriptionType = SubscriptionTypes.Paid, Priority = 200,
                PriceDetails = new Dictionary<string, double>
                {
                    {Rules.SUBSCRIPTION_FIX_PRICE_PROP, Rules.PaidRules.SUBSCRIPTION_FIX_PRICE}
                }
            },
            new Subscription { Id = 2, SubscriptionType = SubscriptionTypes.Free, Priority = 300,
                PriceDetails = new Dictionary<string, double>
                {
                    {Rules.SUBSCRIPTION_FIX_PRICE_PROP, Rules.FreeRules.SUBSCRIPTION_FIX_PRICE }
                }
            },
            new Subscription { Id = 3, SubscriptionType = SubscriptionTypes.Premium, Priority = 100,
                PriceDetails = new Dictionary<string, double>
                {
                    {Rules.SUBSCRIPTION_FIX_PRICE_PROP, Rules.PremiumRules.SUBSCRIPTION_FIX_PRICE }
                }
            },
            new Subscription { Id = 4, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 0,
                PriceDetails = new Dictionary<string, double>
                {
                    {Rules.SUBSCRIPTION_FIX_PRICE_PROP, Rules.CategoryAddictedRules.SUBSCRIPTION_FIX_PRICE }
                },
                BookCategoryId = 2
            },
            new Subscription { Id = 5, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 0,
                PriceDetails = new Dictionary<string, double>
                {
                    { Rules.SUBSCRIPTION_FIX_PRICE_PROP, Rules.CategoryAddictedRules.SUBSCRIPTION_FIX_PRICE }
                },
                BookCategoryId = 1
            },
            new Subscription { Id = 6, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 0,
                PriceDetails = new Dictionary<string, double>
                {
                    { Rules.SUBSCRIPTION_FIX_PRICE_PROP, Rules.CategoryAddictedRules.SUBSCRIPTION_FIX_PRICE }
                },
                BookCategoryId = 3
            }
        };
    }
}
