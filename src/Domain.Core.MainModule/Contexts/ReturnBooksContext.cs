using System.Collections.Generic;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Roles;

namespace Domain.Core.MainModule.Contexts
{
    public class ReturnBooksContext
    {
        public void Interaction(LibraryAccount account, IEnumerable<Book> books)
        {
            var returnner = account.ActAs<IBorrower>();
            foreach (var book in books)
            {
                returnner.ReturnBook(book);
            }
        }
    }
}
