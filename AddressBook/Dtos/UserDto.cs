namespace AddressBook.Dtos
{
    public class UserDto : PersonDto
    {
        public string Password { get; set; } = null!;
    }
}
