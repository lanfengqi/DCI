using System;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Roles;

namespace Domain.Core.MainModule.Entities
{
    public class BorrowInfo : AggregateRoot<Guid>
    {
        public BorrowInfo(Book book, IBorrower borrower, DateTime borrowTime)
            : this(Guid.NewGuid(), book, borrower, borrowTime)
        {
        }
        public BorrowInfo(Guid id, Book book, IBorrower borrower, DateTime borrowTime)
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
