namespace AddressBook.Services.ImportDataService.Interface
{
    public interface IImportDataService
    {
        Task ImportClientsFromCsvAsync(IFormFile csvFile, string userId);
    }
}
