namespace AddressBook.Models
{
    public class UserLoginRecord
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.Now;

        public User User { get; set; }
    }

}
