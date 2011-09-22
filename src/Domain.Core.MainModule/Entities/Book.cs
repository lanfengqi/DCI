using Domain.Core;

namespace Domain.Core.MainModule.Entities
{
    public class Book : AggregateRoot<UniqueId>
    {
        public Book() : this(new UniqueId())
        {
        }
        public Book(UniqueId id) : base(id)
        {
        }

        public string BookName { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
    }
}
