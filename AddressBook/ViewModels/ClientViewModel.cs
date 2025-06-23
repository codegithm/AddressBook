using System.ComponentModel.DataAnnotations;

namespace AddressBook.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = null!;

        [Required(ErrorMessage = "Company is required.")]
        public string Company { get; set; } = null!;

        [Display(Name = "Imported At")]
        [DataType(DataType.DateTime)]
        public DateTime ImportedAt { get; set; }
    }
}

