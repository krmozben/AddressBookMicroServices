using AddressBook.Person.Data.Data.Implementation;
using AddressBook.Person.Service.Model.Request;
using AddressBook.Person.Service.Model.Response;
using AddressBook.Person.Service.Services.Implementation;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AddressBook.Person.Service.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMongoDbDataContext _mongoDbDataContext;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IMongoDbDataContext mongoDbDataContext, ILogger<PersonService> logger)
        {
            _mongoDbDataContext = mongoDbDataContext;
            _logger = logger;
        }

        public async Task CreatePerson(CreatePersonRequest request, string uuid = null)
        {
            if (uuid == null)
                uuid = Guid.NewGuid().ToString();

            await _mongoDbDataContext.PersonModel.InsertOneAsync(new Data.Model.Person
            {
                Name = request.Name,
                LastName = request.LastName,
                Uuid = uuid
            });

            _logger.LogInformation("Added new Person");
        }

        public async Task<List<PersonListResponse>> GetPersons()
        {
            var result = await _mongoDbDataContext.PersonModel.FindAsync(x => true);

            return result.ToList().Select(x => new PersonListResponse
            {
                Uuid = x.Uuid,
                Name = x.Name,
                LastName = x.LastName
            }).ToList();
        }
    }
}
