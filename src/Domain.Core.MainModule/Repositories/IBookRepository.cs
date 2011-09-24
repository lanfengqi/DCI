using System;
using Domain.Core;
using Domain.Core.MainModule.Entities;


namespace Domain.Core.MainModule.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>, IRemoveableRepository<Book, Guid>
    {
    }
}
