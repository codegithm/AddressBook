using AddressBook.Models;
using AddressBook.Repositories.ImportDataRepository.Interface;
using AddressBook.Services.ContactService.Interface;
using AddressBook.Services.ImportDataService.Interface;
using System.Text;

namespace AddressBook.Services.ImportDataService
{
    public class ImportDataService : IImportDataService
    {
        private readonly IContactService _contactService;
        private readonly IImportDataRepository _importDataRepository;
        public ImportDataService(IContactService contactService, IImportDataRepository importDataRepository)
        {
            _contactService = contactService;
            _importDataRepository = importDataRepository;
        }

        public async Task ImportClientsFromCsvAsync(IFormFile csvFile, string userId)
        {
            if (csvFile == null || csvFile.Length == 0)
                throw new ArgumentException("Invalid or empty file provided.");

            using var reader = new StreamReader(csvFile.OpenReadStream());
            string? headerLine = await reader.ReadLineAsync(); // skip header

            var importedClients = new List<Client>();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var parts = line?.Split(',');

                if (parts != null && parts.Length >= 4)
                {
                    importedClients.Add(new Client
                    {
                        Name = parts[0].Trim('"'),
                        EmailAddress = parts[1].Trim('"'),
                        ContactNumber = parts[2].Trim('"'),
                        Company = parts[3].Trim('"'),
                        ImportedAt = DateTime.UtcNow
                    });
                }
            }

            foreach (var client in importedClients)
            {
                await _contactService.AddClientAsync(client);
            }

            var importLog = new DataImportLog
            {
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                LogMessage = "Imported clients contacts"
            };

            await _importDataRepository.CreateImportLogAsync(importLog);
        }

    }
}
