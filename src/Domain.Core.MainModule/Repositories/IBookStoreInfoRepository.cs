using System;
using Domain.Core;
using Domain.Core.MainModule.Entities;

namespace Domain.Core.MainModule.Repositories
{
    public interface IBookStoreInfoRepository : IRepository<BookStoreInfo, Guid>, IRemoveableRepository<BookStoreInfo, Guid>
    {
        BookStoreInfo GetBookStoreInfo(Guid bookId);
    }

}
