using Domain.Core;
using Domain.Core.MainModule.Entities;

namespace Domain.Core.MainModule.Repositories
{
    public interface IBookStoreInfoRepository : IRepository<BookStoreInfo, UniqueId>, IRemoveableRepository<BookStoreInfo, UniqueId>
    {
        BookStoreInfo GetBookStoreInfo(UniqueId bookId);
    }

}
