﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookmark.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int TotalPages { get; set; }
        [Required]
        public int CurrentPage { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();

        [ForeignKey(nameof(Bookshelf))]
        public int BookShelfId { get; set; }
        public virtual Bookshelf Bookshelf { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public decimal Progress
        {
           get
            {
                var progressDecimal = CurrentPage / TotalPages;
                return progressDecimal * 100;
            }
        }

    }
}
