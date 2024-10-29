using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibTrans.Models
{
    public class Books
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [DisplayName("Tytuł")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [DisplayName("Autor")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [DisplayName("ISBN")]
        public string? ISBN { get; set; }

        [DisplayName("Dostępność")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Opis")]
        public string? Description { get; set; }
        

    }
}
