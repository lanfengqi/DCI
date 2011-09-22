using System.Collections.Generic;
using Domain.Core;
using Domain.Core.MainModule.Entities;


namespace Domain.Core.MainModule.Repositories
{
    public interface IBorrowInfoRepository : IRepository<BorrowInfo, UniqueId>, IRemoveableRepository<BorrowInfo, UniqueId>
    {
        IList<BorrowInfo> FindNotReturnedBorrowInfos(UniqueId borrowerId);
        BorrowInfo FindNotReturnedBorrowInfo(UniqueId borrowerId, UniqueId bookId);
    }
}
