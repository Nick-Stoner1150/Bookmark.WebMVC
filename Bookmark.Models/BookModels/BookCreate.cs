using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.BookModels
{
    public class BookCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Please enter at least 1 character!")]
        [MaxLength(50, ErrorMessage = "There are too many characters!")]
        public string Title { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "Please enter at least 1 character!")]
        [MaxLength(50, ErrorMessage = "There are too many characters!")]
        public string Author { get; set; }

        [Required]
        public int TotalPages { get; set; }

        [Required]
        [Display(Name = "Current Page")]
        public int CurrentPage { get; set; }
        public int BookshelfId { get; set; }
        public string UserId { get; set; }
    }
}
