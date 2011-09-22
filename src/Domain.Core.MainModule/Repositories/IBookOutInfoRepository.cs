using Domain.Core;
using Domain.Core.MainModule.Entities;

namespace Domain.Core.MainModule.Repositories
{
    public interface IBookOutInfoRepository : IRepository<BookOutInfo, UniqueId>, IRemoveableRepository<BookOutInfo, UniqueId>
    {
    }
}
