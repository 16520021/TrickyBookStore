using System;
using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            var subscriptions = TrickyBookStore.Services.Store.Subscriptions.Data;
            List<Subscription> result = new List<Subscription>();
            foreach(int id in ids)
            {
                foreach (Subscription subscription in subscriptions)
                {
                    if (id == subscription.Id)
                    {
                        result.Add(subscription);
                    }
                }
            }

            return result;
        }
    }
}
