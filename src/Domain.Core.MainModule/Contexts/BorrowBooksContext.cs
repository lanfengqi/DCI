using System.Collections.Generic;

using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Roles;

namespace Domain.Core.MainModule.Contexts
{
    public class BorrowBooksContext
    {
        public void Interaction(LibraryAccount account, IEnumerable<Book> books)
        {
            var borrower = account.ActAs<IBorrower>();
            foreach (var book in books)
            {
                borrower.BorrowBook(book);
            }
        }
    }
}
