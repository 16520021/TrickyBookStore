using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class BookCategories
    {
        public static readonly IEnumerable<BookCategory> Data = new List<BookCategory>
        {
            new BookCategory {Id = 1, Title = "Document"},
            new BookCategory {Id = 2, Title = "Education"},
            new BookCategory {Id = 3, Title = "Technology" },
            new BookCategory {Id = 4, Title = "Life Style"}
        };
    }
}
