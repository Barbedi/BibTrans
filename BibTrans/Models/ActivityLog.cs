using BibTrans.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibTrans.Models
{
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? Action { get; set; }

        [Required]
        public string ?Controller { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [Required]
        public string? Description { get; set; }

        [ForeignKey("UserId")]
        public virtual BibTransUser? User { get; set; } // Właściwość nawigacyjna do BibTransUser
    }
}
