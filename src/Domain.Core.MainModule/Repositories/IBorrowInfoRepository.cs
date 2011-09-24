using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.Core.MainModule.Entities;


namespace Domain.Core.MainModule.Repositories
{
    public interface IBorrowInfoRepository : IRepository<BorrowInfo, Guid>, IRemoveableRepository<BorrowInfo, Guid>
    {
        IList<BorrowInfo> FindNotReturnedBorrowInfos(Guid borrowerId);
        BorrowInfo FindNotReturnedBorrowInfo(Guid borrowerId, Guid bookId);
    }
}
