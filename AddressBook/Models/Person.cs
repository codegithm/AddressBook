using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}
