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

        [Required]
        public JobCategory Category { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } 

        [Required]
        public string Content { get; set; }

        [Required]
        public PostType Type { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
