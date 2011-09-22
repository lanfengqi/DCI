using System;

namespace Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void RegisterRepository(IPersistableRepository repository);
        void SubmitChanges();
    }
}
