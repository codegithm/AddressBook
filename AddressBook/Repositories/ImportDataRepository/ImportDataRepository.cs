using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Repositories.ImportDataRepository.Interface;

namespace AddressBook.Repositories.ImportDataRepository
{
    public class ImportDataRepository : IImportDataRepository
    {
        private readonly AddressBookContext _context;
        public ImportDataRepository(AddressBookContext context)
        {
            _context = context;
        }
        public async Task CreateImportLogAsync(DataImportLog importLog)
        {
            _context.DataImportLogs.Add(importLog);
            await _context.SaveChangesAsync();
        }
    }
}
