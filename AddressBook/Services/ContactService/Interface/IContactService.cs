using AddressBook.Models;

namespace AddressBook.Services.ContactService.Interface
{
    public interface IContactService
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client?> GetClientAsync(int id);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task<List<string>> GetAutoCompleteNamesAsync(string term);
        Task AddClientAsync(Client client);
        Task<List<Client>> GetClientsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Client>> GetClientByNameAsync(string company);

    }
}
