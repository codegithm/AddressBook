using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = null!;
    }
}
