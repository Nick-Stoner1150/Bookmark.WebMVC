using Bookmark.Data;
using Bookmark.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Services
{
    public class BookshelfServices
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        private readonly string _userId;

        public BookshelfServices(string userId)
        {
            _userId = userId;
        }

        public bool CreateBookshelf(BookshelfCreate model)
        {
            var entity =
                new Bookshelf()
                {
                    UserId = _userId,
                    Name = model.Name,
                    Description = model.Description
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bookshelves.Add(entity);
                return ctx.SaveChanges() == 1;
            }    
        }

        public IEnumerable<BookshelfListItem> GetBookshelves()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bookshelves
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new BookshelfListItem
                                {
                                    BookshelfId = e.BookshelfId,
                                    Name = e.Name,
                                    Description = e.Description,
                                    NumberOfBooks = e.Books.Count
                                });

                return query.ToArray();
            }
        }

        public BookshelfDetail Get(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bookshelf =
                    ctx
                    .Bookshelves
                    .Single(b => b.BookshelfId == id && b.UserId == _userId);

                var query =
                    _db
                    .Books
                    .Where(q => q.BookShelfId == id);

                return new BookshelfDetail
                {
                    BookshelfId = bookshelf.BookshelfId,
                    Name = bookshelf.Name,
                    Description = bookshelf.Name,
                    NumberOfBooks = query.Count()
                };
            }
        }

        public bool UpdateBookshelf(BookshelfEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bookshelves
                        .Single(e => e.BookshelfId == model.BookshelfId && e.UserId == _userId);

                entity.Name = model.Name;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBookshelf(int bookshelfId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bookshelves
                        .Single(e => e.BookshelfId == bookshelfId && e.UserId == _userId);

                ctx.Bookshelves.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
