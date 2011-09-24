using System;
using Domain.Core;
using Domain.Core.MainModule.Entities;

namespace Domain.Core.MainModule.Repositories
{
    public interface IBookOutInfoRepository : IRepository<BookOutInfo, Guid>, IRemoveableRepository<BookOutInfo, Guid>
    {
    }
}
