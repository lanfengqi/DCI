using System;

namespace Domain.Core.MainModule.Entities
{
    public class Book : AggregateRoot<Guid>
    {
        public Book()
            : this(Guid.NewGuid())
        {
        }
        public Book(Guid id)
            : base(id)
        {
        }

        public virtual string BookName { get; set; }
        public virtual string Author { get; set; }
        public virtual string Publisher { get; set; }
        public virtual string ISBN { get; set; }
        public virtual string Description { get; set; }
    }
}
