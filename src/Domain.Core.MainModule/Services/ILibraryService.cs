using System;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Roles;
using Domain.Core;

namespace Domain.Core.MainModule.Services
{
    public interface ILibraryService
    {
        /// <summary>
        /// 将某本书借给某人
        /// </summary>
        void LendBook(Book book, IBorrower borrower);
        /// <summary>
        /// 接收已归还的书
        /// </summary>
        void ReceiveReturnedBook(Book book, IBorrower borrower);

        /// <summary>
        /// 图书入库
        /// </summary>
        void StoreBook(Book book, int count, string location);

        /// <summary>
        /// 图书出库
        /// </summary>
        void OutBook(Book book, int count);

        /// <summary>
        /// 提供某本书的库存信息
        /// </summary>
        BookStoreInfo GetBookStoreInfo(Guid bookId);
    }
}
