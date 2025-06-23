using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Services.AuthService.Interface;
using AddressBook.Services.SystemOverviewService.Interface;
using AddressBook.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ISystemOverviewService _systemOverviewService;
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public HomeController(SignInManager<User> signInManager, AddressBookContext context, ISystemOverviewService systemOverviewService, IAuthService authService, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _systemOverviewService = systemOverviewService;
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var result = await _systemOverviewService.GetSystemOverviewAsync();
            var user = await _userManager.GetUserAsync(User);
            var loginRecords = await _authService.GetUserLoginsCountAsync(user.Email.ToString());

            var homeViewModel = new HomeViewModel
            {
                Name = user.FullName,
                LoginRecord = loginRecords,
                ClientCount = result.ClientCount,
                ExportCount = result.ExportCount,
                NewImportedClients = result.NewImportedClients,
                RecentApiUsage = result.RecentApiUsage
            };
            return View(homeViewModel);


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
