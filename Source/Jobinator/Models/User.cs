using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Jobinator.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
       
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public int LikeCount { get; set; }
    }
}
