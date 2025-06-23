using System.ComponentModel.DataAnnotations;

namespace AddressBook.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; } = null!;
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
