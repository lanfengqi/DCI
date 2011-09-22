using Domain.Core;
using Domain.Core.MainModule.Entities;


namespace Domain.Core.MainModule.Repositories
{
    public interface IBookRepository : IRepository<Book, UniqueId>, IRemoveableRepository<Book, UniqueId>
    {
    }
}
