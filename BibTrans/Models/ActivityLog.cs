using BibTrans.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibTrans.Models
{
    public class ActivityLogs
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string? Action { get; set; }

        public string? Controller { get; set; }

        [Required] // Data zawsze powinna być ustawiona
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public string? Description { get; set; }

        [ForeignKey("UserId")]
        public virtual BibTransUser? User { get; set; } // Nawigacja do BibTransUser
    }
}
