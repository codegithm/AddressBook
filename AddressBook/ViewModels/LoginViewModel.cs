using System.ComponentModel.DataAnnotations;

namespace AddressBook.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; } = null!;
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
