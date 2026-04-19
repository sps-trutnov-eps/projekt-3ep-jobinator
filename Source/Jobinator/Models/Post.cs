using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Jobinator.Models
{
    public class Post
    {
        // enum type for post categories
        public enum JobCategory
        {
            IT,
            Logistics,
            Construction,
            Healthcare,
            Finance,
        }

        public enum PostType
        {
            Offer,
            Demand
        }


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public JobCategory Category { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } 

        [Required(ErrorMessage = "Content is required")]
        [MinLength(10, ErrorMessage = "Content must be at least 10 characters long")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Post type is required")]
        public PostType Type { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
