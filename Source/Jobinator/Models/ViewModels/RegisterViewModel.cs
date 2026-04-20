using System.ComponentModel.DataAnnotations;

namespace Jobinator.Models.ViewModels
{
    // ViewModel pro registraci - obsahuje validační pravidla a popisky pro formulář
    public class RegisterViewModel
    {
        // Uživatelské jméno s validací délky
        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
        [Display(Name = "Username")]
        public required string Username { get; set; }

        // Křestní jméno
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public required string Name { get; set; }

        // Příjmení
        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(100)]
        [Display(Name = "Surname")]
        public required string Surname { get; set; }

        // Heslo s minimální délkou
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        // Kontrola shody hesla
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public required string PasswordCheck { get; set; }
    }
}
