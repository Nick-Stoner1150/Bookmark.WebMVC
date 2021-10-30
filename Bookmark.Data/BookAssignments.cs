using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Data
{
    public class BookAssignments
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        [ForeignKey(nameof(Bookshelf))]
        public int BookshelfId { get; set; }
        public virtual Bookshelf Bookshelf { get; set;  }
    }
}
