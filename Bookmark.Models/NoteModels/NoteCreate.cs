using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.NoteModels
{
    public class NoteCreate
    {
        [Display(Name = "Note Title")]
        public string NoteTitle { get; set; }
        public string Text { get; set; }
        [Display(Name = "Book")]
        public int BookId { get; set; }
    }
}
