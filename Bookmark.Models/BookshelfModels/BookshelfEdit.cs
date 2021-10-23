﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Models.BookshelfModels
{
    public class BookshelfEdit
    {
        public int BookshelfId { get; set; }
        [Display(Name = "Bookshelf Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
