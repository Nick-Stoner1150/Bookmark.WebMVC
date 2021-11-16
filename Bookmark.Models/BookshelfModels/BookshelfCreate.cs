using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.BookshelfModels
{
    public class BookshelfCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Please enter at least 1 character")]
        [MaxLength(20, ErrorMessage = "There are too many characters")]
        [Display(Name = "Bookshelf Name")]
        public string Name { get; set; }

        [MinLength(1, ErrorMessage = "Please enter at least 1 character")]
        [MaxLength(50, ErrorMessage = "There are too many characters")]
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
