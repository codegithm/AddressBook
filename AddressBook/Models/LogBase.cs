namespace AddressBook.Models
{
    public class LogBase
    {
        public int Id { get; set; }
        public string LogMessage { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
