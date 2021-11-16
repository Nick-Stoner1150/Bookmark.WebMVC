using Bookmark.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.BookModels
{
    public class BookDetail
    {
        [Display(Name = "ID")]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        
        [Display(Name = "Total Pages")]
        public int TotalPages { get; set; }

        [Display(Name = "Current Page")]
        public int CurrentPage { get; set; }
        
        [Display(Name = "Bookshelf")]
        public string BookshelfName { get; set; }
        public List<NoteListItem> Notes { get; set; } = new List<NoteListItem>();

        public int BookshelfId { get; set; }

        [Display(Name = "Progress")]
        public decimal BookProgress
        {
            get
            {
                decimal bookProgress = (decimal)CurrentPage / (decimal)TotalPages;
                return bookProgress * 100m;
            }
        }
        [Display(Name = "# of Notes")]
        public int TotalNotes { get; set; }

    }
}
