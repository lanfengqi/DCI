﻿
using System;
using System.Linq;
using Domain.Core;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Infrastructure.CrossCutting.Logging;

namespace Infrastructure.Data.MainModule
{
    public class BookStoreInfoRepository : Repository<BookStoreInfo, Guid>, IBookStoreInfoRepository
    {
        public BookStoreInfoRepository(IUnitOfWork iUnitOfWork, ITraceManager traceManager, IDatabaseFactory databaseFactory)
            : base(iUnitOfWork,traceManager,databaseFactory)
        {
        }

        public BookStoreInfo GetBookStoreInfo(Guid bookId)
        {
            return GetAll().FirstOrDefault(bookStoreInfo => bookStoreInfo.Book.Id == bookId);
        }
    }
}
