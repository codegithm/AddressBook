using System.ComponentModel.DataAnnotations;

namespace AddressBook.Dtos
{
    public class ClientDto : PersonDto
    {
        public string ContactNumber { get; set; } = null!;
        public string Company { get; set; } = null!;
    }
}
