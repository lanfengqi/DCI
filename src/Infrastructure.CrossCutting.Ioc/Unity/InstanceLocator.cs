using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Infrastructure.CrossCutting.Ioc
{
    public class InstanceLocator : IInstanceLocator
    {
        public InstanceLocator()
        { }

        public T GetInstance<T>()
        {
            return UnityContainerHolder.UnityContainer.Resolve<T>();
        }

        public object GetInstance(Type instanceType)
        {
            return UnityContainerHolder.UnityContainer.Resolve(instanceType);
        }

        public bool IsTypeRegistered<T>()
        {
            return UnityContainerHolder.UnityContainer.IsRegistered<T>();
        }

        public bool IsTypeRegistered(Type type)
        {
            return UnityContainerHolder.UnityContainer.IsRegistered(type);
        }

        public void RegisterType<T>()
        {
            UnityContainerHolder.UnityContainer.RegisterType<T>();
        }

        public void RegisterType(Type type)
        {
            UnityContainerHolder.UnityContainer.RegisterType(type);
        }

    }
}
