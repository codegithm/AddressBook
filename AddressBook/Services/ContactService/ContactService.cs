using AddressBook.Models;
using AddressBook.Repositories.ContactRepository.Interface;
using AddressBook.Services.ContactService.Interface;

namespace AddressBook.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            try
            {
                return await _contactRepository.GetAllClientsAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Client?> GetClientAsync(int id)
        {
            try
            {
                return await _contactRepository.GetClientByIdAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                await _contactRepository.UpdateClientAsync(client);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating client", ex);
            }
        }

        public async Task DeleteClientAsync(int id)
        {

            try
            {
               await _contactRepository.DeleteClientAsync(id);

            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting client", ex);
            }
        }
        public async Task AddClientAsync(Client client)
        {
            try
            {
                await _contactRepository.AddClientAsync(client);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the client.", ex);
            }
        }

        public async Task<List<string>> GetAutoCompleteNamesAsync(string term)
        {
            try
            {
                return await _contactRepository.AutoCompleteClientNamesAsync(term);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching autocomplete names", ex);
            }
        }
        public async Task<List<Client>> GetClientsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _contactRepository.GetClientsByDateRangeAsync(startDate, endDate);
        }

        public async Task<List<Client>> GetClientByNameAsync(string company)
        {
            return await _contactRepository.GetClientByNameAsync(company);
        }

    }

}
