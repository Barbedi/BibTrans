using BibTrans.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibTrans.Models
{
    public class Borrowing
    {
        public int Id { get; set; }

        public string UserId { get; set; } 

        [ForeignKey(nameof(UserId))]
        public virtual BibTransUser User { get; set; }

        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Books Book { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
