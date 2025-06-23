using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Services.SystemOverviewService.Interface;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Services.SystemOverviewService
{
    public class SystemOverviewService : ISystemOverviewService
    {
        private readonly AddressBookContext _context;
        public SystemOverviewService(AddressBookContext context)
        {
            _context = context;
        }

        public async Task<SystemOverview> GetSystemOverviewAsync()
        {
            var clientCount = await _context.Clients.CountAsync();

            var exportCount = await _context.DataExportLogs.CountAsync();

            var recentApiCalls = await _context.ApiUsageLogs
                .Where(log => log.Timestamp >= DateTime.UtcNow.AddDays(-7))
                .Distinct()
                .CountAsync();

            var newImports = await _context.DataImportLogs
                .Where(log => log.Timestamp >= DateTime.UtcNow.AddDays(-7))
                .CountAsync();

            return new SystemOverview
            {
                ClientCount = clientCount,
                ExportCount = exportCount,
                NewImportedClients = newImports,
                RecentApiUsage = recentApiCalls
            };
        }
    }
}
