using System.Collections.Generic;
using System.Transactions;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Variables

        public List<IPersistableRepository> PersistableRepositories { get; private set; }

        #endregion

        #region Constructors

        public UnitOfWork()
        {
            PersistableRepositories = new List<IPersistableRepository>();
        }

        #endregion

        #region IUnitOfWork Members

        public void RegisterRepository(IPersistableRepository repository)
        {
            if (!PersistableRepositories.Contains(repository))
            {
                PersistableRepositories.Add(repository);
            }
        }
        public virtual void SubmitChanges()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                foreach (IPersistableRepository repository in PersistableRepositories)
                {
                    repository.PersistChanges();
                }
                transaction.Complete();
            }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
        }

        #endregion
    }
}
