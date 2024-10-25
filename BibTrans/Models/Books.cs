﻿using BibTrans.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibTrans.Models
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }

        public string BorrowedBY { get; set; }

        [ForeignKey(nameof(BorrowedBY))]
        public virtual BibTransUser Borrower { get; set; }

    }
}
