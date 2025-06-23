using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Services.ContactService.Interface;
using AddressBook.Services.ExportDataService.Interface;
using AddressBook.Services.ImportDataService.Interface;
using AddressBook.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AddressBook.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IDataExportService _dataExportService;
        private readonly UserManager<User> _userManager;
        private readonly IImportDataService _importDataService;

        public ContactController(IContactService contactService, IDataExportService dataExportService, UserManager<User> userManager, IImportDataService importDataService)
        {
            _contactService = contactService;
            _dataExportService = dataExportService;
            _userManager = userManager;
            _importDataService = importDataService;
        }

        public async Task<IActionResult> ContactList(string? search)
        {
            List<Client> result;

            if (!string.IsNullOrWhiteSpace(search))
            {
                result = await _contactService.GetClientByNameAsync(search);
            }
            else
            {
                result = await _contactService.GetClientsAsync();
            }

            if (!result.Any())
                return RedirectToAction("AddContact", "Contact");

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_ClientsTable", result);

            return View(result);
        }

        public IActionResult Upload()
        {
             return View();
        }
        public IActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(Client client)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddClientAsync(client);
                return RedirectToAction("ContactList");
            }
            return View(client);
        }
        public async Task<IActionResult> EditContact(int id)
        {
            var client = await _contactService.GetClientAsync(id);
            var clientViewModel = new ClientViewModel();
            if(client != null)
            {
                clientViewModel = new ClientViewModel
                {
                    Id = client.Id,
                    Name = client.Name,
                    ContactNumber = client.ContactNumber,
                    Company = client.Company,
                    EmailAddress = client.EmailAddress,
                    ImportedAt = client.ImportedAt
                };
            }

            return client == null ? NotFound() : View(clientViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    Id = clientViewModel.Id,
                    Name = clientViewModel.Name,
                    ContactNumber = clientViewModel.ContactNumber,
                    EmailAddress = clientViewModel.EmailAddress,
                    Company = clientViewModel.Company,
                    ImportedAt = clientViewModel.ImportedAt
                };

                await _contactService.UpdateClientAsync(client);
                return RedirectToAction("ContactList");
            } 
            return View(clientViewModel);      
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _contactService.DeleteClientAsync(id);
            return RedirectToAction("ContactList");
        }

        [HttpGet]
        public async Task<JsonResult> AutoComplete(string term)
        {
            var names = await _contactService.GetAutoCompleteNamesAsync(term);

            return Json(names);
        }
        public async Task<IActionResult> ExportClientsToCsv()
        {
            var user = await _userManager.GetUserAsync(User);
            var (content, contentType, fileName) = await _dataExportService.ExportClientsToCsvAsync(user.Id);
            return File(content, contentType, fileName);
        }
        public async Task<IActionResult> ImportClientsCsv(IFormFile csvFile) 
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                await _importDataService.ImportClientsFromCsvAsync(csvFile, user.Id);
                return RedirectToAction("ContactList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while importing clients: " + ex.Message);
                return View();
            }
        }

    }

}
