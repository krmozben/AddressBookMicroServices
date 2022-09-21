using AddressBook.Contacts.Application.Services.Implementation;
using AddressBook.Shared.Model.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Contacts.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactRequest request)
        {
            await _contactService.CreateContact(request);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveContact([FromQuery] string Uuid)
        {
            await _contactService.RemoveContact(Uuid);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact([FromBody] UpdateContactRequest request)
        {
            await _contactService.UpdateContact(request);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact([FromQuery] string Uuid)
        {
            await _contactService.DeleteContact(Uuid);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddContactInformationEmail([FromQuery] string Uuid, string content)
        {

            await _contactService.AddContactInformation(new AddContactInformationRequest() { Uuid = Uuid, Content = content, Type = Shared.Enums.InformationType.EmailAdress });

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddContactInformationLocation([FromQuery] string Uuid, string content)
        {

            await _contactService.AddContactInformation(new AddContactInformationRequest() { Uuid = Uuid, Content = content, Type = Shared.Enums.InformationType.Location });

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AddContactInformationPhone([FromQuery] string Uuid, string content)
        {

            await _contactService.AddContactInformation(new AddContactInformationRequest() { Uuid =  Uuid, Content = content, Type = Shared.Enums.InformationType.PhoneNumber });

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveContactInformation([FromQuery] RemoveContactInformationRequest request)
        {

            await _contactService.RemoveContactInformation(request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await _contactService.GetAllContacts());
        }

        [HttpGet]
        public async Task<IActionResult> GetContact([FromQuery] string Uuid)
        {
            return Ok(await _contactService.GetContact(Uuid));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetLocationReport()
        {
            return Ok(await _contactService.GetLocationReport());
        }
    }
}
