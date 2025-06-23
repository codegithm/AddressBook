using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Repositories.ContactRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Repositories.ContactRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AddressBookContext _context;

        public ContactRepository(AddressBookContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            var query = _context.Clients.AsQueryable();

            return await query.ToListAsync();
        }

        public async Task AddClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id) => await _context.Clients.FindAsync(id);

        public async Task UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<string>> AutoCompleteClientNamesAsync(string term)
        {
            return await _context.Clients
                .Where(c => c.Name.Contains(term))
                .Select(c => c.Name)
                .Distinct()
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<Client>> GetClientsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Clients
                .Where(c => c.ImportedAt >= start && c.ImportedAt <= end)
                .ToListAsync();
        }

        public async Task<List<Client>?> GetClientByNameAsync(string name)
        {
            return await _context.Clients
                .Where(c => c.Name == name)
                .ToListAsync();
        }
    }

}
