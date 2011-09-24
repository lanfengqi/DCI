using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using Domain.Core.MainModule.Repositories;
using Infrastructure.CrossCutting.Logging;
using Domain.Core.MainModule.Entities;


namespace Infrastructure.Data.MainModule
{
    public class BorrowInfoRepository : Repository<BorrowInfo, Guid>, IBorrowInfoRepository
    {

        public BorrowInfoRepository(IUnitOfWork iUnitOfWork, ITraceManager traceManager, IDatabaseFactory databaseFactory)
            : base(iUnitOfWork, traceManager,databaseFactory)
        {
        }

        public IList<BorrowInfo> FindNotReturnedBorrowInfos(Guid borrowerId)
        {
            return GetAll().Where(borrowInfo => borrowInfo.LibraryAccount.Id == borrowerId && borrowInfo.ReturnTime == null).ToList();
        }
        public BorrowInfo FindNotReturnedBorrowInfo(Guid borrowerId, Guid bookId)
        {
            return GetAll().FirstOrDefault(borrowInfo => borrowInfo.LibraryAccount.Id == borrowerId && borrowInfo.Book.Id == bookId && borrowInfo.ReturnTime == null);
        }
    }
}
