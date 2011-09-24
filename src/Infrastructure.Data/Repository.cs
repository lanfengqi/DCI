﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

using Domain.Core;
using Infrastructure.CrossCutting.Logging;
using NHibernate;

namespace Infrastructure.Data
{
    public abstract class Repository<TAggregateRoot, TAggregateRootId> :
        IRepository<TAggregateRoot, TAggregateRootId>,
        IRemoveableRepository<TAggregateRoot, TAggregateRootId>,
        IPersistableRepository
        where TAggregateRoot : Entity<TAggregateRootId>, IAggregateRoot<TAggregateRootId>
    {
        #region Private Variables

        private ITraceManager _TraceManager;
        private ISession session;

        #endregion

        #region Constructors

        public Repository(IUnitOfWork iUnitOfWork, ITraceManager traceManager, IDatabaseFactory databaseFactory)
        {
            session = databaseFactory.Current();
            _TraceManager = traceManager;
            iUnitOfWork.RegisterRepository(this);
            _TraceManager.TraceInfo(
                string.Format(CultureInfo.InvariantCulture,
                              Resources.Messages.trace_ConstructRepository,
                              typeof(TAggregateRoot).Name));
        }

        #endregion

        #region IRepository<TAggregateRoot, TAggregateRootId> Members

        public virtual IEnumerable<TAggregateRoot> GetAll()
        {
            ICriteria crit = session.CreateCriteria(typeof(TAggregateRoot));
            return crit.List<TAggregateRoot>();
        }

        public virtual TAggregateRoot Get(TAggregateRootId id)
        {
            return session.Get<TAggregateRoot>(id);
        }

        public virtual void Add(TAggregateRoot aggregateRoot)
        {
            session.Save(aggregateRoot);
            session.Flush();
            _TraceManager.TraceInfo(
                string.Format(CultureInfo.InvariantCulture,
                              Resources.Messages.trace_AddedItemRepository,
                              typeof(TAggregateRoot).Name));

        }

        public virtual void Remove(TAggregateRoot aggregateRoot)
        {
            session.Delete(aggregateRoot);
            session.Flush();
            _TraceManager.TraceInfo(
              string.Format(CultureInfo.InvariantCulture,
                            Resources.Messages.trace_DeletedItemRepository,
                            typeof(TAggregateRoot).Name));
        }

        #endregion

        #region IPersistableRepository Members

        public virtual void PersistChanges()
        {

        }

        #endregion

    }
}
