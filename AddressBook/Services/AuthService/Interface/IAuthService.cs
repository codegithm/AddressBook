using AddressBook.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace AddressBook.Services.AuthService.Interface
{
    public interface IAuthService
    {
        Task<User> SignInAsync(string email, string password, bool remeberMe);
        Task LogoutAsync();
        Task<User> VerifyEmailAsync(string email);
        Task<bool> ChangePasswordAsync(string email, string newPassword);
        Task<IdentityResult> RegisterAsync(User user, string password);
        Task<int> GetUserLoginsCountAsync(string email);
    }
}
