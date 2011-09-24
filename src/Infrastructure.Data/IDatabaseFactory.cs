using System;

using NHibernate;

namespace Infrastructure.Data
{
    public interface IDatabaseFactory : IDisposable
    {
        ISession Current();
    }
}
