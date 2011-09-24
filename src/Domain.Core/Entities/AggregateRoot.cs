using System;
using System.Linq;

namespace Domain.Core
{
    public abstract class AggregateRoot<TAggregateRootId> : Entity<TAggregateRootId>, IAggregateRoot<TAggregateRootId>
    {
        #region Constructor
        public AggregateRoot(TAggregateRootId id)
            : base(id)
        {

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRole"></typeparam>
        /// <returns></returns>
        public virtual TRole ActAs<TRole>() where TRole : class
        {
            object role = null;
            var roleTypeDefinition = typeof(TRole);
            var aggregateRootType = this.GetType();

            if (!roleTypeDefinition.IsInterface)
            {
                throw new NotSupportedException("Role must be an interface.");
            }

            //If the current entity already implemented the role, then return it directly.
            if (roleTypeDefinition.IsAssignableFrom(aggregateRootType))
            {
                return this as TRole;
            }

            var roleType = DomainInitializer.Current.GetActorRoleType(this.GetType(), roleTypeDefinition);
            if (roleType == null)
            {
                throw new NotSupportedException(string.Format("AggregateRoot of type '{0}' cannot act role '{1}'.", aggregateRootType.FullName, roleTypeDefinition.FullName));
            }

            var constructor = roleType.GetConstructors().Where(c => c.GetParameters().Count() == 1 && c.GetParameters()[0].ParameterType == aggregateRootType).SingleOrDefault();
            if (constructor == null)
            {
                throw new NotSupportedException(string.Format("No available constructor found on type '{0}'", roleType.FullName));
            }

            try
            {
                role = constructor.Invoke(new object[] { this }) as TRole;
            }
            catch (Exception ex)
            {
                string error = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception(error);
            }

            return role as TRole;
        }
        #endregion
    }
}
