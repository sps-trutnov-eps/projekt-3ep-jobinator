<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

>>>>>>> 2167cb51bb6461732c6ca2c03f51de3c6b349506
namespace Jobinator.Models
{
    public class User
    {
<<<<<<< HEAD
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
=======
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
>>>>>>> 2167cb51bb6461732c6ca2c03f51de3c6b349506
    }
}
