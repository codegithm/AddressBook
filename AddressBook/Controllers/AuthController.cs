using AddressBook.Models;
using AddressBook.Repositories.AuthRepository.Interface;
using AddressBook.Services.AuthService.Interface;
using AddressBook.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthService authService, IAuthRepository authRepository)
        {
            _authService = authService;
            _authRepository = authRepository;
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _authService.SignInAsync(model.EmailAddress, model.Password, model.RememberMe);
                if (result != null)
                {
                    var loginRecords = await _authService.GetUserLoginsCountAsync(model.EmailAddress);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.VerifyEmailAsync(model.EmailAddress);
                if (result != null)
                {
                    return RedirectToAction("ChangePassword", "Auth", new { username = result.UserName });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something is wrong!");
                    return View(model);
                }
            }
            return View(model);
        }
        public ActionResult ChangePassword(string username)
        {
            if(username == null)
            {
                return RedirectToAction("VerifyEmail", "Auth");
            }
            return View(new ChangePasswordViewModel { EmailAddress = username});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.ChangePasswordAsync(model.EmailAddress, model.NewPassword);
                if (result)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else 
                {                     
                    ModelState.AddModelError(string.Empty, "Something went wrong while changing the password.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FullName = model.Name,
                    Email = model.EmailAddress,
                    UserName = model.EmailAddress,
                };

                var result = await _authService.RegisterAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }
            }
            return View(model);
        }
    }
}
