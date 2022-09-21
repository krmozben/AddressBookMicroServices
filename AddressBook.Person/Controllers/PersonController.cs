using AddressBook.Person.Service.Model.Request;
using AddressBook.Person.Service.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Person.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request)
        {
            await _personService.CreatePerson(request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPersons()
        {
            return Ok(await _personService.GetPersons());
        }
    }
}
