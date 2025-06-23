using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Repositories.AuthRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AddressBookContext _context;
        public AuthRepository(AddressBookContext context)
        {
            _context = context;
        }
        public async Task<bool> AddUserLoginRecordAsync(UserLoginRecord loginRecord)
        {
            try
            {
                _context.UserLoginRecords.Add(loginRecord);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<UserLoginRecord>> FindUserLoginRecordByUserIdAsync(string userId)
        {
            try
            {
                var loginRecords = await _context.UserLoginRecords.ToListAsync();
                var record = loginRecords.FindAll(r => r.UserId == userId);
                return record;
            }
            catch
            {
                return null;
            }
        }
    }
}
