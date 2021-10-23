using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.NoteModels
{
    public class NoteListItem
    {
        [Display(Name = "ID")]
        public int NoteId { get; set; }
        [Display(Name = "Note Title")]
        public string NoteTitle { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedDate { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedDate { get; set; }
        public string BookTitle { get; set; }
    }
}
