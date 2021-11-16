using Bookmark.Models.BookModels;
using System.Collections.Generic;

namespace Bookmark.Services
{
    public interface IBookService
    {
        bool CreateBook(BookCreate model);
        bool DeleteBook(int bookId, string userId);
        BookDetail GetBookById(int id, string userId);
        IEnumerable<BookListItem> GetBooks(string userId);
        bool UpdateBook(BookEdit model);
    }
}