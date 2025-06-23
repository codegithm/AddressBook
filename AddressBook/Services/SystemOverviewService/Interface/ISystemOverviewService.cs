using AddressBook.Models;

namespace AddressBook.Services.SystemOverviewService.Interface
{
    public interface ISystemOverviewService
    {
        Task<SystemOverview> GetSystemOverviewAsync();
    }
}
