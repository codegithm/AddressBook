using AddressBook.Models;

namespace AddressBook.Repositories.ApiUsageDataRepository.Interface
{
    public interface IApiUsageDataRepository
    {
        Task CreateApiUsageLogAsync(ApiUsageLog apiUsageLog);
    }
}
