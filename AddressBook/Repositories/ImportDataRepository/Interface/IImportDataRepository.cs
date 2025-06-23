using AddressBook.Models;

namespace AddressBook.Repositories.ImportDataRepository.Interface
{
    public interface IImportDataRepository
    {
        Task CreateImportLogAsync(DataImportLog exportLog);
    }
}
