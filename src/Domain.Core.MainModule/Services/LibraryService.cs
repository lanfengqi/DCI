using System;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Roles;
using Domain.Core.MainModule.Repositories;


namespace Domain.Core.MainModule.Services
{
    public class LibraryService : ILibraryService
    {
        private IBorrowInfoRepository borrowInfoRepository;
        private IBookStoreInfoRepository bookStoreInfoRepository;
        private IBookOutInfoRepository bookOutInfoRepository;

        public LibraryService(IBorrowInfoRepository borrowInfoRepository, IBookStoreInfoRepository bookStoreInfoRepository, IBookOutInfoRepository bookOutInfoRepository)
        {
            this.bookOutInfoRepository = bookOutInfoRepository;
            this.bookStoreInfoRepository = bookStoreInfoRepository;
            this.borrowInfoRepository = borrowInfoRepository;
        }

        public void LendBook(Book book, LibraryAccount libraryAccount)
        {
            var bookStoreInfo = bookStoreInfoRepository.GetBookStoreInfo(book.Id);
            if (bookStoreInfo.Count == 0)
            {
                throw new Exception(string.Format("The count of book '{0}' in library is zero, so you cannot borrow it.", book.BookName));
            }
            bookStoreInfo.DecreaseCount(); //数量减1

            //生成借书信息并保存到Repository中
            borrowInfoRepository.Add(new BorrowInfo(book, libraryAccount, DateTime.Now));
        }

        public void ReceiveReturnedBook(Book book, LibraryAccount libraryAccount)
        {
            //设置借书信息的还书时间
            var borrowedInfo = borrowInfoRepository.FindNotReturnedBorrowInfo(libraryAccount.Id, book.Id);
            borrowedInfo.ReturnTime = DateTime.Now;

            //这里，真正的系统还会计算归还时间是否超期，计算罚款之类的逻辑，因为我这个是一个演示的例子，所以不做这个处理了

            //这里只更新书本的数量信息，因为还书时并不是马上把书本放回书架的，所以此时书本的书架位置信息还是保留为空
            //等到我们将这本书放到书架的某个位置时，才会更新其位置信息
            var bookStoreInfo = bookStoreInfoRepository.GetBookStoreInfo(book.Id);
            bookStoreInfo.IncreaseCount(); //数量加1
        }

        public void StoreBook(Book book, int count, string location)
        {
            //图书入库时生成图书的库存信息
            var bookStoreInfo = new BookStoreInfo(book, count);
            bookStoreInfo.Location = location;
            bookStoreInfoRepository.Add(bookStoreInfo);
        }

        public void OutBook(Book book, int count)
        {

            var bookStoreInfo = bookStoreInfoRepository.GetBookStoreInfo(book.Id);
            if (bookStoreInfo.Count == 0 || bookStoreInfo.Count < count)
            {
                throw new Exception(string.Format("The count of book '{0}' in library is zero, so you cannot borrow it.", book.BookName));
            }
            bookStoreInfo.DecreaseCount(count);

            //图书去库信息并保存到Repository中
            bookOutInfoRepository.Add(new BookOutInfo(book, count, DateTime.Now));
        }

        public BookStoreInfo GetBookStoreInfo(Guid bookId)
        {
            return bookStoreInfoRepository.GetBookStoreInfo(bookId);
        }
    }
}
