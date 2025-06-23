using AddressBook.Models;

namespace AddressBook.ViewModels
{
    public class HomeViewModel : SystemOverview
    {
        public int LoginRecord { get; set; }
        public string Name { get; set; }
    }
}
