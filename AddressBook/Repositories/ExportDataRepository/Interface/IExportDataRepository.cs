using AddressBook.Models;

namespace AddressBook.Repositories.ExportDataRepository.Interface
{
    public interface IExportDataRepository
    {
        Task CreateExportLogAsync(DataExportLog exportLog);
    }
}
