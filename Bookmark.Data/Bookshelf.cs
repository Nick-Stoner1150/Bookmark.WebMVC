using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Data
{
    public class Bookshelf
    {
        [Key]
        public int BookshelfId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
