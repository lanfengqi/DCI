using System;
using Domain.Core.MainModule.Entities;

namespace Domain.Core.MainModule.Entities
{
    public class BookStoreInfo : AggregateRoot<Guid>
    {
        public BookStoreInfo(Book book, int count)
            : this(Guid.NewGuid(), book, count)
        {
        }
        public BookStoreInfo(Guid id, Book book, int count)
            : base(id)
        {
            this.Book = book;
            this.Count = count;
        }

        protected BookStoreInfo()
            : base(Guid.NewGuid())
        { }

        public virtual Book Book { get; private set; }
        public virtual int Count { get; private set; }
        public virtual string Location { get; set; }

        public virtual void IncreaseCount()
        {
            this.Count++;
        }
        public virtual void DecreaseCount()
        {
            this.Count--;
        }

        public virtual void DecreaseCount(int count)
        {
            this.Count -= count;
        }
    }
}
