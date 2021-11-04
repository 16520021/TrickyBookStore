using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Utils;

namespace TrickyBookStore.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        ICustomerService CustomerService { get; }
        IPurchaseTransactionService PurchaseTransactionService { get; }

        private double paymentAmount = 0;
        public PaymentService(ICustomerService customerService, 
            IPurchaseTransactionService purchaseTransactionService)
        {
            CustomerService = customerService;
            PurchaseTransactionService = purchaseTransactionService;
        }

        private double CalculateNewBooksPricesWithSubscriptions(List<Book> listOfNewBooks, List<Subscription> subscriptions)
        {
            for (int i = 0; i < listOfNewBooks.Count(); i++)
            {
                bool priceCalculated = false;
                var newBook = listOfNewBooks[i];
                foreach (var sub in subscriptions)
                {
                    if (priceCalculated) break;
                    switch (sub.SubscriptionType)
                    {
                        case SubscriptionTypes.CategoryAddicted:
                            if (newBook.CategoryId == sub.BookCategoryId)
                            {
                                if (i < Rules.CategoryAddictedRules.BOOK_QUANTITY_APPLY_DISCOUNT_CATEGORY)
                                {
                                    paymentAmount += newBook.Price * Rules.CategoryAddictedRules.NEW_BOOK_PRICE_PREFIX;
                                    priceCalculated = true;
                                }
                                else paymentAmount += newBook.Price;
                            }
                            break;
                        case SubscriptionTypes.Premium:
                            if (i < Rules.PremiumRules.BOOK_QUANTITY_APPLY_DISCOUNT_PREMIUM)
                            {
                                paymentAmount += newBook.Price * Rules.PremiumRules.NEW_BOOK_PRICE_PREFIX;
                                priceCalculated = true;
                            }
                            else paymentAmount += newBook.Price;
                            break;
                        case SubscriptionTypes.Paid:
                            if (i < Rules.PaidRules.BOOK_QUANTITY_APPLY_DISCOUNT_PAID)
                            {
                                paymentAmount += (newBook.Price * Rules.PaidRules.NEW_BOOK_PRICE_PREFIX);
                                priceCalculated = true;
                            }
                            else paymentAmount += newBook.Price;
                            break;
                        case SubscriptionTypes.Free:
                            paymentAmount += newBook.Price;
                            priceCalculated = true;
                            break;
                        default:
                            break;
                    }
                }
            }

            return paymentAmount;
        }

        private double CalculateOldBooksPricesWithSubscriptions(List<Book> listOfOldBook, List<Subscription> subscriptions)
        {
            var subscriptionTypes = subscriptions.Select(sub => sub.SubscriptionType).ToList();
            if (!subscriptionTypes.Contains(SubscriptionTypes.Premium))
            {
                for (int i = 0; i < listOfOldBook.Count(); i++)
                {
                    bool priceCalculated = false;
                    var oldBook = listOfOldBook[i];
                    foreach (var sub in subscriptions)
                    {
                        if (priceCalculated) break;
                        switch (sub.SubscriptionType)
                        {
                            case SubscriptionTypes.CategoryAddicted:
                                if (oldBook.CategoryId == sub.BookCategoryId)
                                {
                                    paymentAmount += 0;
                                    priceCalculated = true;
                                }
                                break;
                            case SubscriptionTypes.Paid:
                                paymentAmount += oldBook.Price * Rules.PaidRules.OLD_BOOK_PRICE_PREFIX;
                                priceCalculated = true;
                                break;
                            case SubscriptionTypes.Free:
                                paymentAmount += oldBook.Price * Rules.FreeRules.OLD_BOOK_PRICE_PREFIX;
                                priceCalculated = true;
                                break;
                            default:
                                paymentAmount += oldBook.Price;
                                break;
                        }
                    }
                }
            }

            return paymentAmount;
        }

        public double GetPaymentAmount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var transactions = PurchaseTransactionService.GetPurchaseTransactions(customerId, fromDate, toDate);
            var ascendingTransaction = transactions.OrderBy(transaction => transaction.CreatedDate);
            var customer = CustomerService.GetCustomerById(customerId);
            var ascendingSubs = customer.Subscriptions.OrderBy(sub => sub.Priority);

            var transactionsNewBook = ascendingTransaction
                                     .Select(transaction => transaction.Book)
                                     .Where(book => !book.IsOld).ToList();

            var transactionsOldBook = ascendingTransaction
                                     .Select(transaction => transaction.Book)
                                     .Where(book => book.IsOld).ToList();

            if (customer != null)
            {
                CalculateNewBooksPricesWithSubscriptions(transactionsNewBook, ascendingSubs.ToList());

                CalculateOldBooksPricesWithSubscriptions(transactionsOldBook, ascendingSubs.ToList());

                //Calcute subscription price
                foreach (var sub in ascendingSubs)
                {
                    paymentAmount += sub.PriceDetails[Rules.SUBSCRIPTION_FIX_PRICE_PROP];
                }
            }

            return paymentAmount;
        }
    }
}
