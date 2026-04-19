using System.ComponentModel.DataAnnotations;

namespace Jobinator.Models.ViewModels
{
    // ViewModel pro přihlašovací formulář
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
