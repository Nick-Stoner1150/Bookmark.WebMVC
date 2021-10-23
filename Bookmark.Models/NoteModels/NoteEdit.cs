using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.NoteModels
{
    public class NoteEdit
    {
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        public string Text { get; set; }
    }
}
