using System.Collections.Generic;

namespace Domain.Core
{
    /// <summary>
    ///  值对象接口定义
    /// </summary>
    public interface IValueObject
    {
        IEnumerable<object> GetAtomicValues();
    }
}