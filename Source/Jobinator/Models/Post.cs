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


        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public JobCategory Category { get; set; } // Later could be changed to EnumDataType

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } 

        [Required]
        public string Content { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
