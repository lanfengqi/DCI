using System;

namespace Domain.Core
{
    /// <summary>
    /// 实体
    /// </summary>
    /// <typeparam name="TEntityId"></typeparam>
    public abstract class Entity<TEntityId> : IEntity<TEntityId>
    {
        public Entity(TEntityId id)
        {
            this.Id = id;
        }

        public virtual TEntityId Id { get; private set; }
    }
}
