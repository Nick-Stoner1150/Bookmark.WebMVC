using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.BookModels
{
    public class BookListItem
    {
        [Display(Name = "ID")]
        public int BookId { get; set; }
        public string Title { get; set; }

        [Display(Name = "Total Pages")]
        public int TotalPages { get; set; }

        [Display(Name = "Current Page")]
        public int CurrentPage { get; set; }

        [Display(Name = "Bookshelf")]
        public string BookshelfName { get; set; }
        // public int Notes { get; set; }
        [Display(Name = "Progress")]
        public decimal BookProgress
        {
            get
            {
                decimal bookProgress = (decimal)CurrentPage / (decimal)TotalPages;
                return bookProgress * 100m;

            }
        }
    }
}
