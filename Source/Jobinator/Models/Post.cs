using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Jobinator.Models;

namespace Jobinator.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public PostType Type { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } 

        [Required]
        public string Content { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public enum PostType { Offer, Request }
        public enum JobCategory { IT, Marketing, Finance }
        public JobCategory Category { get; set; }
    }
}
