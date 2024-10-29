using System.ComponentModel.DataAnnotations;

namespace BibTrans.Models
{
    public class Books
    {
        
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "Title is required")]
            public string? Title { get; set; }

            [Required(ErrorMessage = "Author is required")]
            public string? Author { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
            public string? ISBN { get; set; }

            public bool IsAvailable { get; set; }

            [Required(ErrorMessage = "Description is required")]
            public string? Description { get; set; }
        

    }
}
