using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Data.MainModule
{
    public class BookOutInfoRepository : Repository<BookOutInfo, UniqueId>, IBookOutInfoRepository
    {
        public BookOutInfoRepository(IUnitOfWork iUnitOfWork)
            : base(iUnitOfWork)
        {
        }
    }
}
