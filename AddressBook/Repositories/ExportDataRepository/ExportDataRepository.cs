using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Repositories.ExportDataRepository.Interface;

namespace AddressBook.Repositories.ExportDataRepository
{
    public class ExportDataRepository : IExportDataRepository
    {
        private readonly AddressBookContext _context;
        public ExportDataRepository(AddressBookContext context)
        {
            _context = context;
        }
        public async Task CreateExportLogAsync(DataExportLog exportLog)
        {
            _context.DataExportLogs.Add(exportLog);
            await _context.SaveChangesAsync();
        }
    }
}
