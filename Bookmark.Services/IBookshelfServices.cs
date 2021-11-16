using Bookmark.Models.BookshelfModels;
using System.Collections.Generic;

namespace Bookmark.Services
{
    public interface IBookshelfServices
    {
        bool CreateBookshelf(BookshelfCreate model);
        bool DeleteBookshelf(int bookshelfId, string userId);
        BookshelfDetail Get(int id, string userId);
        IEnumerable<BookshelfListItem> GetBookshelves(string userId);
        bool UpdateBookshelf(BookshelfEdit model);
    }
}