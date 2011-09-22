
using System.Linq;
using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Data.MainModule
{
    public class BookStoreInfoRepository : Repository<BookStoreInfo, UniqueId>, IBookStoreInfoRepository
    {
        public BookStoreInfoRepository(IUnitOfWork iUnitOfWork)
            : base(iUnitOfWork)
        {
        }

        public BookStoreInfo GetBookStoreInfo(UniqueId bookId)
        {
            return GetAll().FirstOrDefault(bookStoreInfo => bookStoreInfo.Book.Id == bookId);
        }
    }
}
