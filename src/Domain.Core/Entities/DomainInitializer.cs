using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Domain.Core
{
    public class DomainInitializer
    {
        #region Members
        private static DomainInitializer current = new DomainInitializer();
        private Dictionary<ActorRoleMappingItem, Type> mappings = new Dictionary<ActorRoleMappingItem, Type>();
        private Dictionary<Type, List<Type>> eventSubscriberTypeMappings = new Dictionary<Type, List<Type>>();
        private Dictionary<Type, IEnumerable<MethodInfo>> subscriberEventHandlers = new Dictionary<Type, IEnumerable<MethodInfo>>();
        private List<Type> repositoryTypeList = new List<Type>();
        #endregion

        #region Public Properties
        public static DomainInitializer Current
        {
            get { return current; }
        }

        #endregion

        #region Public Methods

        public void InitializeDomain(Assembly assembly)
        {
            ResolveRepositories(assembly);
            ResolveRoles(assembly);
        }
        public void ResolveRepositories(Assembly assembly)
        {
            repositoryTypeList = assembly.GetTypes().Where(type => type.IsInterface && type.GetInterfaces().Any(i => IsRepositoryDefinition(i))).ToList();
        }
        public Type GetRepositoryType(Type aggregateRootType)
        {
            foreach (var repositoryType in repositoryTypeList)
            {
                if (GetAggregateRootType(repositoryType) == aggregateRootType)
                {
                    return repositoryType;
                }
            }
            return null;
        }
        public void ResolveRoles(Assembly assembly)
        {
            var allRoleImplementationTypes = assembly.GetTypes().Where(type => type.IsClass && type.GetInterfaces().Any(i => IsRoleDefinition(i)));
            foreach (var roleImplementationType in allRoleImplementationTypes)
            {
                var actorType = GetActorType(roleImplementationType);
                foreach (var roleTypeDefinition in roleImplementationType.GetInterfaces().Where(i => IsRoleDefinition(i)))
                {
                    if (mappings.Keys.ToList().Exists(item => item.ActorType == actorType && item.RoleTypeDefinition == roleTypeDefinition))
                    {
                        throw new Exception(string.Format("Actor of type '{0}' cannot act with role '{1}' twice.", actorType.FullName, roleTypeDefinition.FullName));
                    }
                    mappings[new ActorRoleMappingItem { ActorType = actorType, RoleTypeDefinition = roleTypeDefinition }] = roleImplementationType;
                }
            }
        }
        public Type GetActorRoleType(Type actorType, Type roleTypeDefinition)
        {
            return mappings.Where(item => item.Key.ActorType == actorType && item.Key.RoleTypeDefinition == roleTypeDefinition).Select(item => item.Value).SingleOrDefault();
        }
        public void ResolveDomainEvents(Assembly assembly)
        {
            foreach (var subscriberType in assembly.GetTypes().Where(type => type.IsClass && type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Count() == 0))
            {
                var eventHandlers = subscriberType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(IsEventHandler);
                subscriberEventHandlers.Add(subscriberType, eventHandlers);
                foreach (var eventHandler in eventHandlers)
                {
                    var eventType = eventHandler.GetParameters()[0].ParameterType;
                    List<Type> subscriberTypes = null;
                    if (!eventSubscriberTypeMappings.TryGetValue(eventType, out subscriberTypes))
                    {
                        subscriberTypes = new List<Type>();
                        eventSubscriberTypeMappings.Add(eventType, subscriberTypes);
                    }
                    if (!subscriberTypes.Exists(existingSubscriberType => existingSubscriberType == subscriberType))
                    {
                        subscriberTypes.Add(subscriberType);
                    }
                }
            }
        }
        public List<Type> GetSubscriberTypesList(Type eventType)
        {
            //Get all the subscribers which subscribe the current event type
            //or subscribe the event types which can be assigned from the current event type.
            var subscriberTypesList = eventSubscriberTypeMappings.ToList().FindAll(pair => pair.Key.IsAssignableFrom(eventType)).Select(pair => pair.Value).ToList();

            //Merge all the subscriber types into one list.
            List<Type> mergedSubscriberTypes = new List<Type>();
            foreach (var subscriberTypes in subscriberTypesList)
            {
                foreach (var subscriberType in subscriberTypes)
                {
                    if (!mergedSubscriberTypes.Exists(mst => mst == subscriberType))
                    {
                        mergedSubscriberTypes.Add(subscriberType);
                    }
                }
            }

            //Return all the merged subscriber types.
            return mergedSubscriberTypes;
        }
        public IEnumerable<MethodInfo> GetEventHandlers(Type subscriberType)
        {
            IEnumerable<MethodInfo> eventHandlers = null;
            if (!subscriberEventHandlers.TryGetValue(subscriberType, out eventHandlers))
            {
                eventHandlers = new List<MethodInfo>();
            }
            return eventHandlers;
        }

        #endregion

        #region Private Methods

        private Type GetAggregateRootType(Type repositoryType)
        {
            return repositoryType.GetInterfaces().Single(i => IsRepositoryDefinition(i)).GetGenericArguments()[0];
        }
        private bool IsRepositoryDefinition(Type type)
        {
            if (type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IRepository<,>))
            {
                return true;
            }
            return false;
        }
        private bool IsRoleDefinition(Type type)
        {
            if (type.IsInterface)
            {
                if (type.IsGenericType)
                {
                    if (type.GetGenericTypeDefinition() == typeof(IRole<>))
                    {
                        return false;
                    }
                    else if (type.GetInterfaces().Any(i => i.GetGenericTypeDefinition() == typeof(IRole<>)))
                    {
                        return true;
                    }
                }
                else if (type.GetInterfaces().Any(i => i.GetGenericTypeDefinition() == typeof(IRole<>)))
                {
                    return true;
                }
            }
            return false;
        }
        private Type GetActorType(Type roleImplementationType)
        {
            Type baseType = roleImplementationType.BaseType;

            while (baseType != typeof(object) && !(baseType.IsGenericType && (baseType.GetGenericTypeDefinition() == typeof(Role<,>))))
            {
                baseType = baseType.BaseType;
            }

            if (baseType.IsGenericType && (baseType.GetGenericTypeDefinition() == typeof(Role<,>)))
            {
                return baseType.GetGenericArguments()[0];
            }

            return roleImplementationType;
        }
        private bool IsEventHandler(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters();
            return parameters.Count() == 1 && typeof(IDomainEvent).IsAssignableFrom(parameters[0].ParameterType);
        }

        #endregion

        private class ActorRoleMappingItem
        {
            public Type ActorType { get; set; }
            public Type RoleTypeDefinition { get; set; }
        }
    }
}
