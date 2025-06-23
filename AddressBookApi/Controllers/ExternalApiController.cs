using AddressBook.Services.ApiService.Interface;
using AddressBook.Services.ContactService.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalApiController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IApiService _apiService;

        public ExternalApiController(IContactService contactService, IApiService apiService)
        {
            _contactService = contactService;
            _apiService = apiService;
        }

        [HttpGet("clients")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetClients([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            await _apiService.AddApiUseLog("External api log hit");
            var clients = await _contactService.GetClientsByDateRangeAsync(startDate, endDate);
            return Ok(clients);
        }
    }

}
