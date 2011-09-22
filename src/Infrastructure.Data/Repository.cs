using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Domain.Core;

namespace Infrastructure.Data
{
    public abstract class Repository<TAggregateRoot, TAggregateRootId> :
        IRepository<TAggregateRoot, TAggregateRootId>,
        IRemoveableRepository<TAggregateRoot, TAggregateRootId>,
        IPersistableRepository
        where TAggregateRoot : Entity<TAggregateRootId>, IAggregateRoot<TAggregateRootId>
    {
        #region Private Variables
        private List<TrackingObject> trackingObjectList = new List<TrackingObject>();
        #endregion

        #region Constructors

        public Repository(IUnitOfWork iUnitOfWork)
        {
            iUnitOfWork.RegisterRepository(this);
        }

        #endregion

        #region IRepository<TAggregateRoot, TAggregateRootId> Members

        public virtual IEnumerable<TAggregateRoot> GetAll()
        {
            TrackAggregateRoots(GetAllFromPersistence());
            return GetTrackingAggregateRoots().ToList();
        }
        public virtual TAggregateRoot Get(TAggregateRootId id)
        {
            //Check whether the aggregateRoot is removed.
            if (IsAggregateRootRemoved(id))
            {
                return null;
            }

            //Try to get the aggregateRoot from memory.
            var trackingObject = (from trackingObj in trackingObjectList where Equals(trackingObj.Id, id) && trackingObj.Status != AggregateRootStatus.Removed select trackingObj).FirstOrDefault();
            if (trackingObject != null)
            {
                return trackingObject.CurrentValue;
            }

            //If we still cannot find the aggregateRoot, then get it from persistence.
            TAggregateRoot aggregateRoot = GetFromPersistence(id);
            if (aggregateRoot != null)
            {
                TrackingAggregateRoot(aggregateRoot);
            }

            return aggregateRoot;
        }
        public virtual void Add(TAggregateRoot aggregateRoot)
        {
            if ((from trackingObj in trackingObjectList where (trackingObj.Status == AggregateRootStatus.New || trackingObj.Status == AggregateRootStatus.Tracking) && Equals(trackingObj.Id, aggregateRoot.Id) select trackingObj).FirstOrDefault() != null)
            {
                throw new InvalidOperationException("The aggregateRoot already exists.");
            }
            else
            {
                var trackingObject = (from trackingObj in trackingObjectList where trackingObj.Status == AggregateRootStatus.Removed && Equals(trackingObj.Id, aggregateRoot.Id) select trackingObj).FirstOrDefault();
                if (trackingObject != null)
                {
                    trackingObject.CurrentValue = aggregateRoot;
                    trackingObject.TrackingProperties = CreateDictionary(aggregateRoot);
                    trackingObject.Status = AggregateRootStatus.Tracking;
                }
                else
                {
                    trackingObjectList.Add(new TrackingObject { Id = aggregateRoot.Id, CurrentValue = aggregateRoot, Status = AggregateRootStatus.New });
                }
            }
        }
        public virtual void Remove(TAggregateRoot aggregateRoot)
        {
            var trackingObject = (from trackingObj in trackingObjectList where trackingObj.Status == AggregateRootStatus.New && Equals(trackingObj.Id, aggregateRoot.Id) select trackingObj).FirstOrDefault();
            if (trackingObject != null)
            {
                trackingObjectList.Remove(trackingObject);
            }
            else
            {
                trackingObject = (from trackingObj in trackingObjectList where trackingObj.Status == AggregateRootStatus.Tracking && Equals(trackingObj.Id, aggregateRoot.Id) select trackingObj).FirstOrDefault();
                if (trackingObject != null)
                {
                    trackingObject.Status = AggregateRootStatus.Removed;
                }
            }
        }

        #endregion

        #region IPersistableRepository Members

        public virtual void PersistChanges()
        {
            PersistNewAggregateRoots(GetNewAggregateRoots());
            PersistModifiedAggregateRoots(GetModifiedAggregateRoots());
            PersistRemovedAggregateRoots(GetRemovedAggregateRoots());
        }

        #endregion

        #region Protected Methods

        protected void TrackingAggregateRoot(TAggregateRoot aggregateRoot)
        {
            if (aggregateRoot != null)
            {
                var trackAggregateRoot = trackingObjectList.Find(tar => Equals(tar.Id, aggregateRoot.Id));
                if (trackAggregateRoot == null)
                {
                    trackingObjectList.Add(
                        new TrackingObject
                        {
                            Id = aggregateRoot.Id,
                            CurrentValue = aggregateRoot,
                            TrackingProperties = CreateDictionary(aggregateRoot),
                            Status = AggregateRootStatus.Tracking
                        });
                }
            }
        }
        protected void TrackAggregateRoots(IEnumerable<TAggregateRoot> aggregateRoots)
        {
            if (aggregateRoots != null)
            {
                aggregateRoots.ForEach(aggregateRoot => TrackingAggregateRoot(aggregateRoot));
            }
        }
        protected IList<TAggregateRoot> GetTrackingAggregateRoots()
        {
            return (from trackingObject in trackingObjectList where trackingObject.Status != AggregateRootStatus.Removed select trackingObject.CurrentValue).ToList();
        }
        protected IList<TAggregateRoot> GetAggregateRoots(IEnumerable<TAggregateRoot> aggregateRootsFromDataPersistence, Func<TAggregateRoot, bool> predicate)
        {
            TrackAggregateRoots(aggregateRootsFromDataPersistence);
            return GetTrackingAggregateRoots().Where(predicate).ToList();
        }
        protected virtual IList<TAggregateRoot> GetAllFromPersistence()
        {
            return new List<TAggregateRoot>();
        }
        protected virtual TAggregateRoot GetFromPersistence(TAggregateRootId id)
        {
            return null;
        }
        protected virtual void PersistNewAggregateRoots(List<TAggregateRoot> newAggregateRoots)
        {
        }
        protected virtual void PersistModifiedAggregateRoots(List<TAggregateRoot> modifiedAggregateRoots)
        {
        }
        protected virtual void PersistRemovedAggregateRoots(List<TAggregateRoot> removedAggregateRoots)
        {
        }

        #endregion

        #region Private Methods

        private bool IsAggregateRootRemoved(TAggregateRootId id)
        {
            return (from trackingObject in trackingObjectList where trackingObject.Status == AggregateRootStatus.Removed && Equals(trackingObject.Id, id) select trackingObject).FirstOrDefault() != null;
        }
        private Dictionary<string, object> CreateDictionary(object sourceAggregateRoot)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var propertyInfo in GetTrackingProperties(sourceAggregateRoot))
            {
                var value = propertyInfo.GetValue(sourceAggregateRoot, null);
                if (value == null)
                {
                    dictionary.Add(propertyInfo.Name, null);
                }
                else if (value is IEnumerable)
                {
                    List<object> subValueList = new List<object>();
                    IEnumerator enumerator = ((IEnumerable)value).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        subValueList.Add(CreateDictionary(enumerator.Current));
                    }
                    dictionary.Add(propertyInfo.Name, subValueList);
                }
                else if (value.GetType().IsByRef)
                {
                    dictionary.Add(propertyInfo.Name, CreateDictionary(value));
                }
                else
                {
                    dictionary.Add(propertyInfo.Name, value);
                }
            }
            return dictionary;
        }
        private bool IsAggregateRootModified(TrackingObject trackingObject)
        {
            if (trackingObject.Status == AggregateRootStatus.Tracking && trackingObject.CurrentValue != null)
            {
                foreach (var propertyInfo in GetTrackingProperties(trackingObject.CurrentValue))
                {
                    //TODO, 支持递归
                    var backupValue = trackingObject.TrackingProperties[propertyInfo.Name];
                    var currentValue = propertyInfo.GetValue(trackingObject.CurrentValue, null);
                    if (!IsValueEqual(backupValue, currentValue))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsValueEqual(object firstValue, object secondValue)
        {
            if (ReferenceEquals(firstValue, null) ^ ReferenceEquals(secondValue, null))
            {
                return false;
            }
            if (ReferenceEquals(firstValue, null))
            {
                return true;
            }
            if (firstValue.GetType() != secondValue.GetType())
            {
                return false;
            }

            if (firstValue is IEnumerable && secondValue is IEnumerable)
            {
                var firstValueEnumerator = ((IEnumerable)firstValue).GetEnumerator();
                var secondValueEnumerator = ((IEnumerable)secondValue).GetEnumerator();
                var hasFirstSubValue = firstValueEnumerator.MoveNext();
                var hasSecondSubValue = secondValueEnumerator.MoveNext();

                while (hasFirstSubValue && hasSecondSubValue)
                {
                    if (!IsValueEqual(firstValueEnumerator.Current, secondValueEnumerator.Current))
                    {
                        return false;
                    }
                    hasFirstSubValue = firstValueEnumerator.MoveNext();
                    hasSecondSubValue = secondValueEnumerator.MoveNext();
                }
                if (!hasFirstSubValue && !hasSecondSubValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (firstValue is IEnumerable && !(secondValue is IEnumerable))
            {
                return false;
            }
            else if (!(firstValue is IEnumerable) && secondValue is IEnumerable)
            {
                return false;
            }

            //TODO
            //var valueType = firstValue.GetType();
            //if (valueType.IsValueType || valueType == typeof(string) || valueType.IsClass && typeof(IValueAggregateRoot).IsAssignableFrom(valueType))
            //{
            //    return firstValue == secondValue;
            //}

            return false;
        }
        private List<PropertyInfo> GetTrackingProperties(object aggregateRoot)
        {
            return (from propertyInfo in aggregateRoot.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) select propertyInfo).ToList();
        }
        private List<TAggregateRoot> GetNewAggregateRoots()
        {
            return (from trackingObject in trackingObjectList where trackingObject.Status == AggregateRootStatus.New select trackingObject.CurrentValue).ToList();
        }
        private List<TAggregateRoot> GetModifiedAggregateRoots()
        {
            return (from trackingObject in trackingObjectList where IsAggregateRootModified(trackingObject) select trackingObject.CurrentValue).ToList();
        }
        private List<TAggregateRoot> GetRemovedAggregateRoots()
        {
            return (from trackingObject in trackingObjectList where trackingObject.Status == AggregateRootStatus.Removed select trackingObject.CurrentValue).ToList();
        }

        #endregion

        private class TrackingObject
        {
            public TAggregateRootId Id { get; set; }
            public TAggregateRoot CurrentValue { get; set; }
            public Dictionary<string, object> TrackingProperties { get; set; }
            public AggregateRootStatus Status { get; set; }
        }
        private enum AggregateRootStatus
        {
            New,
            Tracking,
            Removed
        }
    }
}
