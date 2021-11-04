using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;

namespace TrickyBookStore.Services.PurchaseTransactions
{
    public class PurchaseTransactionService : IPurchaseTransactionService
    {
        IBookService BookService { get; }

        public PurchaseTransactionService(IBookService bookService)
        {
            BookService = bookService;
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var purchaseTransactions = TrickyBookStore.Services.Store.PurchaseTransactions.Data;

            List<PurchaseTransaction> qualifiedTransaction = new List<PurchaseTransaction>();
            foreach(var transaction in purchaseTransactions)
            {
                if (transaction.CustomerId == customerId 
                    && transaction.CreatedDate >= fromDate 
                    && transaction.CreatedDate <= toDate)
                {
                    var book = BookService.GetBooks(transaction.BookId);
                    transaction.Book = book[0]; 
                    qualifiedTransaction.Add(transaction);
                }
            }

            return qualifiedTransaction;
        }
    }
}
