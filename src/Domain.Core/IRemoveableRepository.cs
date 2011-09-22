
namespace Domain.Core
{
    public interface IRemoveableRepository<TAggregateRoot, TAggregateRootId>
         where TAggregateRoot : Entity<TAggregateRootId>, IAggregateRoot<TAggregateRootId>
    {
        void Remove(TAggregateRoot aggregateRoot);
    }
}
