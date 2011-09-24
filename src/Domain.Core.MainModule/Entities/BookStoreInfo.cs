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

        public Book Book { get; private set; }
        public int Count { get; private set; }
        public string Location { get; set; }

        public void IncreaseCount()
        {
            this.Count++;
        }
        public void DecreaseCount()
        {
            this.Count--;
        }

        public void DecreaseCount(int count)
        {
            this.Count -= count;
        }
    }
}
