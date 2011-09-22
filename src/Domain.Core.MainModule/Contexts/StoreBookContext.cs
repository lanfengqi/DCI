using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Services;

namespace Domain.Core.MainModule.Contexts
{
    public class StoreBookContext
    {
        private ILibraryService library = null;
        private Book book = null;

        public StoreBookContext(ILibraryService library, Book book)
        {
            this.library = library;
            this.book = book;
        }

        public void Interaction(int count, string location)
        {
            library.StoreBook(book, count, location);
        }


    }
}
