using System;
using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Books
{
    public class BookService : IBookService
    {
        public IList<Book> GetBooks(params long[] ids)
        {
            var books = TrickyBookStore.Services.Store.Books.Data;
            List<Book> result = new List<Book>();
            foreach (long id in ids)
            {
                foreach(Book book in books)
                {
                    if (book.Id == id) result.Add(book);
                }
            }

            return result;
        }
    }
}
