using System.ComponentModel.DataAnnotations;

namespace Jobinator.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string PasswordCheck { get; set; }
    }
}
