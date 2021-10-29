using Bookmark.Data;
using Bookmark.Models.BookModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.BookshelfModels
{
    public class BookshelfDetail
    {
        [Display(Name = "ID")]
        public int BookshelfId { get; set; }
        [Display(Name = "Bookshelf Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

    }
}
