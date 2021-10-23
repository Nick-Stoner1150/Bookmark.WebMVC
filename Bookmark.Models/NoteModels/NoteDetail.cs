using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.NoteModels
{
    public class NoteDetail
    {
        public int NoteId { get; set; }
        [Display(Name = "Note Title")]
        public string NoteTitle { get; set; }
        public string Text { get; set; }
        public string Book { get; set; }
    }
}
