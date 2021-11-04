using System;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        ISubscriptionService SubscriptionService { get; }

        public CustomerService(ISubscriptionService subscriptionService)
        {
            SubscriptionService = subscriptionService;
        }

        public Customer GetCustomerById(long id)
        {
            var customers = TrickyBookStore.Services.Store.Customers.Data;
            foreach(Customer customer in customers)
            {
                if (customer.Id == customer.Id)
                {
                    var customerSubscriptions = SubscriptionService.GetSubscriptions(customer.SubscriptionIds.ToArray());
                    customer.Subscriptions = customerSubscriptions;
                    return customer;
                }
            }
            return null;
        }
    }
}
