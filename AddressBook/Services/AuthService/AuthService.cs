using AddressBook.Models;
using AddressBook.Repositories.AuthRepository.Interface;
using AddressBook.Services.AuthService.Interface;
using Microsoft.AspNetCore.Identity;

namespace AddressBook.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthRepository _authRepository;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IAuthRepository authRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authRepository = authRepository;
        }
        public async Task<bool> ChangePasswordAsync(string email, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(email);
                if (user != null)
                {
                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        var addPasswordResult = await _userManager.AddPasswordAsync(user, newPassword);
                        return addPasswordResult.Succeeded;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetUserLoginsCountAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var loginRecords = await _authRepository.FindUserLoginRecordByUserIdAsync(user.Id);
                    return loginRecords.Count;
                }
                else
                {
                    throw new Exception("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user login count.", ex);
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging out.", ex);
            }
        }

        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the user.", ex);
            }
        }

        public async Task<User> SignInAsync(string email, string password, bool rememberMe)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    var loginRecord = new UserLoginRecord
                    {
                        UserId = user.Id,
                        LoginTime = DateTime.UtcNow
                    };
                    var res = await _authRepository.AddUserLoginRecordAsync(loginRecord);
                    if (res)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while signing in.", ex);
            }
        }

        public async Task<User> VerifyEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(email);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
