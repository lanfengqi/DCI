using System.Collections.Generic;

namespace Domain.Core
{
    public interface IRepository<TAggregateRoot, TAggregateRootId>
        where TAggregateRoot : Entity<TAggregateRootId>, IAggregateRoot<TAggregateRootId>
    {

        IEnumerable<TAggregateRoot> GetAll();
        TAggregateRoot Get(TAggregateRootId id);
        void Add(TAggregateRoot aggregateRoot);
    }

}
