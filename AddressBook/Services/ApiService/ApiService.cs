using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Repositories.ApiUsageDataRepository.Interface;
using AddressBook.Services.ApiService.Interface;

namespace AddressBook.Services.ApiService
{
    public class ApiService : IApiService
    {
        private readonly IApiUsageDataRepository _apiUsageDataRepository;
        public ApiService(IApiUsageDataRepository apiUsageDataRepository)
        {
            _apiUsageDataRepository = apiUsageDataRepository;
        }

        public async Task AddApiUseLog(string message)
        {
            var log = new ApiUsageLog
            {
                LogMessage = message,
            };
            await _apiUsageDataRepository.CreateApiUsageLogAsync(log);
        }
    }
}
