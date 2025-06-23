using AddressBook.Models;

namespace AddressBook.Repositories.ContactRepository.Interface
{
    public interface IContactRepository
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(int id);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
        Task<List<string>> AutoCompleteClientNamesAsync(string term);
        Task AddClientAsync(Client client);
        Task<List<Client>> GetClientsByDateRangeAsync(DateTime start, DateTime end);
        Task<List<Client>?> GetClientByNameAsync(string name);
    }
}
