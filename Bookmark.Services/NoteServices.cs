using Bookmark.Data;
using Bookmark.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Services
{
    public class NoteServices
    {
        private readonly string _userId;

        public NoteServices(string userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    UserId = _userId,
                    NoteTitle = model.NoteTitle,
                    Text = model.Text,
                    BookId = model.BookId,
                    CreatedDate = DateTime.Now
                    
                };

            using (var ctx = new ApplicationDbContext())
            {
                var book = ctx.Books.Find(model.BookId);
                if (book is null)
                {
                    return false;
                }

                entity.Book = book;
                entity.Book.Notes.Add(entity);

                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.UserId == _userId)
                        .Select(
                        e =>
                        new NoteListItem
                        {
                            NoteId = e.NoteId,
                            NoteTitle = e.NoteTitle,
                            CreatedDate = e.CreatedDate,
                            ModifiedDate = e.ModifiedDate,
                            BookTitle = e.Book.Title,
                        });

                return query.ToArray();

            }
        }

        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == id && e.UserId == _userId);

                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        NoteTitle = entity.NoteTitle,
                        Text = entity.Text,
                        Book = entity.Book.Title
                    };
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId && e.UserId == _userId);

                entity.NoteTitle = model.NoteTitle;
                entity.Text = model.Text;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == id && e.UserId == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
