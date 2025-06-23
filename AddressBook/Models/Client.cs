using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class Client 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string Company { get; set; } = null!;
        public DateTime ImportedAt { get; set; } = DateTime.UtcNow;
    }
}
