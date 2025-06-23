using AddressBook.Models;
using AddressBook.Repositories.ExportDataRepository.Interface;
using AddressBook.Services.ContactService.Interface;
using AddressBook.Services.ExportDataService.Interface;
using System.Text;

namespace AddressBook.Services.DataExportService
{
    public class DataExportService : IDataExportService
    {
        private readonly IContactService _contactService;
        private readonly IExportDataRepository _exportDataRepository;
        public DataExportService(IContactService contactService, IExportDataRepository exportDataRepository)
        {
            _contactService = contactService;
            _exportDataRepository = exportDataRepository;
        }
        public async Task<(byte[] Content, string ContentType, string FileName)> ExportClientsToCsvAsync(string userId)
        {
            var clients = await _contactService.GetClientsAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Name,EmailAddress,ContactNumber,Company,ImportedAt");

            foreach (var c in clients)
            {
                sb.AppendLine($"\"{c.Name}\",\"{c.EmailAddress}\",\"{c.ContactNumber}\",\"{c.Company}\",\"{c.ImportedAt:yyyy-MM-dd HH:mm}\"");
            }

            var logEntry = new DataExportLog
            {
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                LogMessage = "Exported clients contacts"
            };
            await _exportDataRepository.CreateExportLogAsync(logEntry);
            var content = Encoding.UTF8.GetBytes(sb.ToString());
            return (content, "text/csv", "Clients.csv");
        }
    }
}
