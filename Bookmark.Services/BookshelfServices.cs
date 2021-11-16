using Bookmark.Data;
using Bookmark.Models.BookshelfModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Services
{
    public class BookshelfServices : IBookshelfServices
    {
        private ApplicationDbContext _db = new ApplicationDbContext();


        public bool CreateBookshelf(BookshelfCreate model)
        {
            var entity =
                new Bookshelf()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Description = model.Description
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bookshelves.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BookshelfListItem> GetBookshelves(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bookshelves
                        .Where(e => e.UserId == userId)
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

        public BookshelfDetail Get(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bookshelf =
                    ctx
                    .Bookshelves
                    .Single(b => b.BookshelfId == id && b.UserId == userId);

                var query =
                    _db
                    .Books
                    .Where(q => q.BookShelfId == id);

                return new BookshelfDetail
                {
                    BookshelfId = bookshelf.BookshelfId,
                    Name = bookshelf.Name,
                    Description = bookshelf.Description,
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
                        .Single(e => e.BookshelfId == model.BookshelfId && e.UserId == model.UserId);

                entity.Name = model.Name;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBookshelf(int bookshelfId, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bookshelves
                        .Single(e => e.BookshelfId == bookshelfId && e.UserId == userId);

                ctx.Bookshelves.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
