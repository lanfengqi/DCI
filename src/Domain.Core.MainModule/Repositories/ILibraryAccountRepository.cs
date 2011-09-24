using System;
using Domain.Core;
using Domain.Core.MainModule.Entities;


namespace Domain.Core.MainModule.Repositories
{
    public interface ILibraryAccountRepository : IRepository<LibraryAccount, Guid>, IRemoveableRepository<LibraryAccount, Guid>
    {
    }
}
