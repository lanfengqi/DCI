using Domain.Core.MainModule.Entities;

namespace Domain.Core.MainModule.Entities
{
    public class LibraryAccount : AggregateRoot<UniqueId>
    {
        public LibraryAccount(string number) : this(new UniqueId(), number)
        {
        }
        public LibraryAccount(UniqueId id, string number) : base(id)
        {
        }

        public string Number { get; private set; }
        public string OwnerName { get; set; }
        public bool IsLocked { get; set; }
    }
}
