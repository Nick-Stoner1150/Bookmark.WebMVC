using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
