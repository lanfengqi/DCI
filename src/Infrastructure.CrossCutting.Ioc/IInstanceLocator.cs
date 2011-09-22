using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.CrossCutting.Ioc
{
    public interface IInstanceLocator
    {
        T GetInstance<T>();
        object GetInstance(Type type);
        bool IsTypeRegistered<T>();
        bool IsTypeRegistered(Type type);
        void RegisterType<T>();
        void RegisterType(Type type);
    }
}
