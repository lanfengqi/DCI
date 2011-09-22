
using System.Linq;
using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Infrastructure.CrossCutting.Logging;

namespace Infrastructure.Data.MainModule
{
    public class BookStoreInfoRepository : Repository<BookStoreInfo, UniqueId>, IBookStoreInfoRepository
    {
        public BookStoreInfoRepository(IUnitOfWork iUnitOfWork, ITraceManager traceManager)
            : base(iUnitOfWork,traceManager)
        {
        }

        public BookStoreInfo GetBookStoreInfo(UniqueId bookId)
        {
            return GetAll().FirstOrDefault(bookStoreInfo => bookStoreInfo.Book.Id == bookId);
        }
    }
}
