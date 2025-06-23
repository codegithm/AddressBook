using AddressBook.Models;

namespace AddressBook.Repositories.AuthRepository.Interface
{
    public interface IAuthRepository
    {
        Task<bool> AddUserLoginRecordAsync(UserLoginRecord loginRecord);
        Task<List<UserLoginRecord>> FindUserLoginRecordByUserIdAsync(string userId);
    }
}
