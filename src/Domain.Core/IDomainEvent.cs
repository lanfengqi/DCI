using System.Collections.Generic;

namespace Domain.Core
{
    /// <summary>
    /// 领域事件接口定义
    /// </summary>
    public  interface IDomainEvent
    {
        IList<object> Results { get; }
        T GetTypedResult<T>();
        IList<T> GetTypedResults<T>();
    }
}
