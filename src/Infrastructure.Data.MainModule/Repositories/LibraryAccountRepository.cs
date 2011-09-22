using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Infrastructure.CrossCutting.Logging;


namespace Infrastructure.Data.MainModule
{

    public class LibraryAccountRepository : Repository<LibraryAccount, UniqueId>, ILibraryAccountRepository
    {
        public LibraryAccountRepository(IUnitOfWork iUnitOfWork, ITraceManager traceManager)
            : base(iUnitOfWork, traceManager)
        {
        }
    }
}
