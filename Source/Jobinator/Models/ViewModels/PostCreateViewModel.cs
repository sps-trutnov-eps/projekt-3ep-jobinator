using System.ComponentModel.DataAnnotations;
using static Jobinator.Models.Post;

namespace Jobinator.Models.ViewModels
{
    // ViewModel pro vytvoření nového příspěvku (nabídka/poptávka)
    public class PostCreateViewModel
    {
        [Required(ErrorMessage = "Please select a category")]
        public JobCategory Category { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [MinLength(10, ErrorMessage = "Content must be at least 10 characters long")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please select whether this is an Offer or a Demand")]
        public PostType Type { get; set; }
    }
}
