using System;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Roles;

namespace Domain.Core.MainModule.Entities
{
    public class BorrowInfo : AggregateRoot<UniqueId>
    {
        public BorrowInfo(Book book, IBorrower borrower, DateTime borrowTime) : this(new UniqueId(), book, borrower, borrowTime)
        {
        }
        public BorrowInfo(UniqueId id, Book book, IBorrower borrower, DateTime borrowTime)
            : base(id)
        {
            this.Book = book;
            this.Borrower = borrower;
            this.BorrowTime = borrowTime;
        }

        public Book Book { get; private set; }
        public IBorrower Borrower { get; private set; }
        public DateTime BorrowTime { get; private set; }
        public DateTime? ReturnTime { get; set; }
    }
}
