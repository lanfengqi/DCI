using Domain.Core.MainModule.Entities;

namespace Domain.Core.MainModule.Entities
{
    public class BookStoreInfo : AggregateRoot<UniqueId>
    {
        public BookStoreInfo(Book book, int count)
            : this(new UniqueId(), book, count)
        {
        }
        public BookStoreInfo(UniqueId id, Book book, int count)
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
