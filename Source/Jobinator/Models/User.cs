using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Jobinator.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(100)]
        [Display(Name = "Username")]
        public required string Username { get; set; }
       
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(100)]
        [Display(Name = "Surname")]
        public required string Surname { get; set; }

        [Required]
        [MaxLength(256)]
        public required string PasswordHash { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}
