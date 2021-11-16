using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.BookModels
{
    public class BookEdit
    {
        public int BookId { get; set; }
        public int CurrentPage { get; set; }
        public int BookshelfId { get; set; }
        public string UserId { get; set; }
    }
}
