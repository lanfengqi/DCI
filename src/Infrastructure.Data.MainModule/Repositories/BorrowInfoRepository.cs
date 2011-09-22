using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using Domain.Core.MainModule.Repositories;
using Infrastructure.Data;
using Domain.Core.MainModule.Entities;


namespace Infrastructure.Data.MainModule
{
    public class BorrowInfoRepository : Repository<BorrowInfo, UniqueId>, IBorrowInfoRepository
    {

        public BorrowInfoRepository(IUnitOfWork iUnitOfWork)
            : base(iUnitOfWork)
        {
        }

        public IList<BorrowInfo> FindNotReturnedBorrowInfos(UniqueId borrowerId)
        {
            return GetAll().Where(borrowInfo => borrowInfo.Borrower.Id == borrowerId && borrowInfo.ReturnTime == null).ToList();
        }
        public BorrowInfo FindNotReturnedBorrowInfo(UniqueId borrowerId, UniqueId bookId)
        {
            return GetAll().FirstOrDefault(borrowInfo => borrowInfo.Borrower.Id == borrowerId && borrowInfo.Book.Id == bookId && borrowInfo.ReturnTime == null);
        }
    }
}
