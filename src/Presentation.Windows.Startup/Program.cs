using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.Core.MainModule.Contexts;
using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Repositories;
using Domain.Core.MainModule.Services;
using Infrastructure.Data;
using Infrastructure.CrossCutting.Ioc;
using Infrastructure.Data.MainModule;
using Microsoft.Practices.Unity;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.CrossCutting.NetFramework.Logging;


namespace Presentation.Windows.Startup
{
    class Program
    {
        private static IBookRepository bookRepository = null;
        private static ILibraryAccountRepository libraryAccountRepository = null;
        private static IBorrowInfoRepository borrowInfoRepository = null;
        private static ILibraryService library = null;


        static void Main(string[] args)
        {
            InitializeIocAndFramework();
            BorrowReturnBookExample();

            Console.WriteLine("");
            Console.WriteLine("按任意键退出......");
            Console.Read();
        }

        private static void InitializeIocAndFramework()
        {
            //初始化IoC, 将IoC保存到静态变量中，以便永久保存该对象
            IUnityContainer container = new UnityContainer();
            UnityContainerHolder.UnityContainer = container;

            IUnityContainer unityContainer = container.RegisterType<IUnitOfWork, UnitOfWork>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IEventPublisher, EventPublisher>(new ContainerControlledLifetimeManager());

            container.RegisterType<ILibraryService, LibraryService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILibraryAccountRepository, LibraryAccountRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBookRepository, BookRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBorrowInfoRepository, BorrowInfoRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBookStoreInfoRepository, BookStoreInfoRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBookOutInfoRepository, BookOutInfoRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITraceManager, TraceManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new ContainerControlledLifetimeManager());

            //初始化Domain
            DomainInitializer.Current.InitializeDomain(typeof(Book).Assembly);
            DomainInitializer.Current.ResolveDomainEvents(typeof(Book).Assembly);

            bookRepository = IoCFactory.Instance.CurrentContainer.GetInstance<IBookRepository>();
            libraryAccountRepository = IoCFactory.Instance.CurrentContainer.GetInstance<ILibraryAccountRepository>();
            borrowInfoRepository = IoCFactory.Instance.CurrentContainer.GetInstance<IBorrowInfoRepository>();
            library = IoCFactory.Instance.CurrentContainer.GetInstance<ILibraryService>();
        }
        private static void BorrowReturnBookExample()
        {
            PrintDescriptionBeforeExample();

            //创建2本书
            var book1 = new Book { BookName = "C#高级编程", Author = "Jhon Smith", ISBN = "56-YAQ-23452", Publisher = "清华大学出版社", Description = "A very good book." };
            var book2 = new Book { BookName = "JQuery In Action", Author = "Jhon Smith", ISBN = "09-BEH-23452", Publisher = "人民邮电出版社", Description = "A very good book." };
            

            bookRepository.Add(book1);
            bookRepository.Add(book2);

            //创建一个图书馆用户帐号，用户凭帐号借书
            var libraryAccount = new LibraryAccount(GenerateAccountNumber(10)) { OwnerName = "赖小天", IsLocked = false };
            libraryAccountRepository.Add(libraryAccount);
            PrintAccountInfo(libraryAccount);

            //创建并启动图书入库场景
            PrintDescriptionBeforeStoreBookContext(book1, book2);
            new StoreBookContext(library, book1).Interaction(2, "4F-S-0001");
            new StoreBookContext(library, book2).Interaction(3, "4F-N-0002");
            PrintBookCount(book1, book2);

            //创建并启动借书场景
            PrintDescriptionBeforeContext1(libraryAccount, book1, book2);
            new BorrowBooksContext().Interaction(libraryAccount, new List<Book> { book1, book2 });
            PrintBorrowedInfo(libraryAccount);
            PrintBookCount(book1, book2);

            //创建并启动还书场景
            PrintDescriptionBeforeContext2(libraryAccount, book1);
            new ReturnBooksContext().Interaction(libraryAccount, new List<Book> { book1 });
            PrintBorrowedInfo(libraryAccount);
            PrintBookCount(book1, book2);

            //创建并启动图书出库
            PrintDescriptionBeforeOutBookContext(book1, book2);
            new OutBookContext(library, book1).Interaction(1);
            new OutBookContext(library, book2).Interaction(1);
            PrintBookCount(book1, book2);
        }

        #region Helper Methods

        private static void PrintDescriptionBeforeStoreBookContext(Book book1, Book book2)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("图书入库场景：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("将两本书入库到图书馆：");
            Console.Write(string.Format("【{0}】", book1.BookName));
            Console.Write(string.Format("【{0}】", book2.BookName));
        }

        private static void PrintDescriptionBeforeOutBookContext(Book book1, Book book2)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("图书出库场景：");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("将两本书从图书馆出库：");
            Console.Write(string.Format("【{0}】", book1.BookName));
            Console.Write(string.Format("【{0}】", book2.BookName));
        }

        private static void PrintDescriptionBeforeExample()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("图书借阅系统领域建模示例");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("");
        }
        private static void PrintAccountInfo(LibraryAccount libraryAccount)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(string.Format("创建了一个图书馆用户帐号：{0}，帐号拥有者：{1}", libraryAccount.Number, libraryAccount.OwnerName));
        }
        private static void PrintDescriptionBeforeContext1(LibraryAccount libraryAccount, Book book1, Book book2)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(string.Format("借书场景：帐号{0}借了两本书：", libraryAccount.Number));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(book1.BookName);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("和");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(book2.BookName);
        }
        private static void PrintDescriptionBeforeContext2(LibraryAccount libraryAccount, Book book1)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(string.Format("还书场景：帐号{0}还了一本书：", libraryAccount.Number));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(book1.BookName);
        }
        private static void PrintBorrowedInfo(LibraryAccount libraryAccount)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine(string.Format("帐号{0}当前借的书本：", libraryAccount.Number));
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var borrowedInfo in borrowInfoRepository.FindNotReturnedBorrowInfos(libraryAccount.Id))
            {
                Console.WriteLine("书名：{0}", borrowedInfo.Book.BookName);
            }
        }
        private static void PrintBookCount(Book book1, Book book2)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("");
            Console.Write("书本数量信息：");
            Console.Write(string.Format("【{0}】：{1}本；", book1.BookName, library.GetBookStoreInfo(book1.Id).Count));
            Console.Write(string.Format("【{0}】：{1}本；", book2.BookName, library.GetBookStoreInfo(book2.Id).Count));
        }
        private static int rep = 0;
        private static string GenerateAccountNumber(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return str;
        }

        #endregion
    }
}
