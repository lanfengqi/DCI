using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.Data;

namespace Infrastructure.Data.MainModule
{
    public class BookRepository : Repository<Book, UniqueId>, IBookRepository
    {
        public BookRepository(IUnitOfWork iUnitOfWork, ITraceManager traceManager)
            : base(iUnitOfWork, traceManager)
        {
        }
    }
}
