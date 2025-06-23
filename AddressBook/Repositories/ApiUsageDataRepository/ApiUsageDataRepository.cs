using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Repositories.ApiUsageDataRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Repositories.ApiUsageDataRepository
{
    public class ApiUsageDataRepository : IApiUsageDataRepository
    {
        private readonly AddressBookContext _context;
        public ApiUsageDataRepository(AddressBookContext context)
        {
            _context = context;
        }
        public async Task CreateApiUsageLogAsync(ApiUsageLog apiUsageLog)
        {
            _context.ApiUsageLogs.Add(apiUsageLog);
            await _context.SaveChangesAsync();
        }
    }
}
