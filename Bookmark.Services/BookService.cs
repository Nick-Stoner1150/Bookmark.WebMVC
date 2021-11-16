using Bookmark.Data;
using Bookmark.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Services
{
    public class BookService : IBookService
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public bool CreateBook(BookCreate model)
        {
            var entity =
                new Book()
                {
                    UserId = model.UserId,
                    Title = model.Title,
                    Author = model.Author,
                    TotalPages = model.TotalPages,
                    CurrentPage = model.CurrentPage,
                    BookShelfId = model.BookshelfId

                };

            using (var ctx = new ApplicationDbContext())
            {
                var bookshelf = ctx.Bookshelves.Find(model.BookshelfId);
                if (bookshelf is null)
                {
                    return false;
                }

                entity.Bookshelf = bookshelf;
                entity.Bookshelf.Books.Add(entity);

                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookListItem> GetBooks(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Books
                        .Where(e => e.UserId == userId)
                        .Select(
                        e =>
                            new BookListItem
                            {
                                BookId = e.BookId,
                                Title = e.Title,
                                TotalPages = e.TotalPages,
                                CurrentPage = e.CurrentPage,
                                BookshelfName = e.Bookshelf.Name
                            });

                return query.ToArray();
            }
        }

        public BookDetail GetBookById(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookId == id && e.UserId == userId);

                var query =
                       _db
                           .Notes
                           .Where(e => e.BookId == id).ToArray();

                return
                    new BookDetail
                    {
                        BookId = entity.BookId,
                        Title = entity.Title,
                        Author = entity.Author,
                        TotalPages = entity.TotalPages,
                        CurrentPage = entity.CurrentPage,
                        BookshelfName = entity.Bookshelf.Name,
                        BookshelfId = entity.BookShelfId,
                        TotalNotes = query.Count()
                    };
            }
        }

        public bool UpdateBook(BookEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookId == model.BookId && e.UserId == model.UserId);

                entity.CurrentPage = model.CurrentPage;
                entity.BookShelfId = model.BookshelfId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBook(int bookId, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Books
                        .Single(e => e.BookId == bookId && e.UserId == userId);

                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
