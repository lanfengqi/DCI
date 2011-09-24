using System;
using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Infrastructure.CrossCutting.Logging;


namespace Infrastructure.Data.MainModule
{

    public class LibraryAccountRepository : Repository<LibraryAccount, Guid>, ILibraryAccountRepository
    {
        public LibraryAccountRepository(IUnitOfWork iUnitOfWork, ITraceManager traceManager, IDatabaseFactory databaseFactory)
            : base(iUnitOfWork, traceManager,databaseFactory)
        {
        }
    }
}
