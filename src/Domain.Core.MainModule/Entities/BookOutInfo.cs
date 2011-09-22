using System;
using Domain.Core;

namespace Domain.Core.MainModule.Entities
{
    public class BookOutInfo : AggregateRoot<UniqueId>
    {
        public BookOutInfo(Book book, int count, DateTime outTime)
            : this(new UniqueId(), book, count, outTime)
        {

        }

        public BookOutInfo(UniqueId id, Book book, int count, DateTime outTime)
            : base(id)
        {
            this.Book = book;
            this.Count = count;
            this.OutTime = outTime;
        }

        public Book Book { get; private set; }
        public int Count { get; private set; }
        public DateTime? OutTime { get; set; }
    }
}
