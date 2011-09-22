using System.Collections.Generic;

namespace Domain.Core
{
    /// <summary>
    /// 事件生成接口定义
    /// </summary>
    public interface IEventPublisher
    {
        void Publish(IDomainEvent evnt);
        void Publish(IEnumerable<IDomainEvent> evnts);
        void Publish(params IDomainEvent[] evnts);
    }
}
