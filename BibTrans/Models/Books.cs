using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibTrans.Models
{
    public class Books
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [DisplayName("Tytuł")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Autor jest wymagany")]
        [DisplayName("Autor")]
        public string? Autor { get; set; }

        [Required(ErrorMessage = "Numer ISBN jest wymagany")]
        [DisplayName("ISBN")]
        public string? ISBN { get; set; }

        [DisplayName("Dostępność")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [DisplayName("Opis")]
        public string? Description { get; set; }
        

    }
}
