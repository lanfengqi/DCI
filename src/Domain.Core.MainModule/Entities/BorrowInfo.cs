using System;

using Domain.Core.MainModule.Roles;

namespace Domain.Core.MainModule.Entities
{
    public class BorrowInfo : AggregateRoot<Guid>
    {
        public BorrowInfo(Book book, LibraryAccount libraryAccount, DateTime borrowTime)
            : this(Guid.NewGuid(), book, libraryAccount, borrowTime)
        {
        }
        public BorrowInfo(Guid id, Book book, LibraryAccount libraryAccount, DateTime borrowTime)
            : base(id)
        {
            this.Book = book;
            this.LibraryAccount = libraryAccount;
            this.BorrowTime = borrowTime;
        }

        protected  BorrowInfo():base(Guid.NewGuid())
        {}

        public virtual Book Book { get; private set; }
        public virtual LibraryAccount LibraryAccount { get; private set; }
        public virtual DateTime BorrowTime { get; private set; }
        public virtual DateTime? ReturnTime { get; set; }
    }
}
