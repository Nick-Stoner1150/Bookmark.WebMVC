using Bookmark.Models.NoteModels;
using System.Collections.Generic;

namespace Bookmark.Services
{
    public interface INoteServices
    {
        bool CreateNote(NoteCreate model);
        bool DeleteNote(int id, string userId);
        NoteDetail GetNoteById(int id, string userId);
        IEnumerable<NoteListItem> GetNotes(string userId);
        bool UpdateNote(NoteEdit model);
    }
}