namespace Domain.Core
{
    /// <summary>
    /// 实体接口定义
    /// </summary>
    /// <typeparam name="TEntityId"></typeparam>
    public interface IEntity<TEntityId>
    {
        TEntityId Id { get; }
    }
}
