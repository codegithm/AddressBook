namespace AddressBook.Services.ExportDataService.Interface
{
    public interface IDataExportService
    {
        Task<(byte[] Content, string ContentType, string FileName)> ExportClientsToCsvAsync(string userId);
    }
}
