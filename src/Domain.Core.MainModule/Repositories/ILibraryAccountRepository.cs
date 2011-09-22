using Domain.Core;
using Domain.Core.MainModule.Entities;


namespace Domain.Core.MainModule.Repositories
{
    public interface ILibraryAccountRepository : IRepository<LibraryAccount, UniqueId>, IRemoveableRepository<LibraryAccount, UniqueId>
    {
    }
}
