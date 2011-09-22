namespace Domain.Core
{
    /// <summary>
    /// 根接口定义
    /// </summary>
    /// <typeparam name="TAggregateRootId"></typeparam>
    public interface IAggregateRoot<TAggregateRootId> : IEntity<TAggregateRootId>
    {
    }
}
