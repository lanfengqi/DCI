using Domain.Core.MainModule.Entities;
using Domain.Core.MainModule.Services;

namespace Domain.Core.MainModule.Contexts
{
    public class OutBookContext
    {
        private ILibraryService library = null;
        private Book book = null;

        public OutBookContext(ILibraryService library, Book book)
        {
            this.library = library;
            this.book = book;
        }

        public void Interaction(int count)
        {
            library.OutBook(book, count);
        }
    }
}
