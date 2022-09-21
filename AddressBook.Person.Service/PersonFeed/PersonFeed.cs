using AddressBook.Person.Data.Data.Implementation;
using AddressBook.Person.Service.Services.Implementation;

namespace AddressBook.Person.Service.PersonFeed
{
    public  class PersonFeed
    {
        private readonly IMongoDbDataContext _mongoDbDataContext;
        private readonly IPersonService _personService;

        public PersonFeed(IMongoDbDataContext mongoDbDataContext, IPersonService personService)
        {
            _mongoDbDataContext = mongoDbDataContext;
            _personService = personService;
        }

        public async Task SeedAsync()
        {
            var persons = await _personService.GetPersons();
            if (persons != null && persons.Count > 0)
                return;

            await _personService.CreatePerson(new() { Name = "Kerim", LastName = "Özben" }, "9e8ab117-c11f-4053-86ff-a37612047f11");
            await _personService.CreatePerson(new() { Name = "Halil", LastName = "Tayfur" }, "9e8ab117-c11f-4053-86ff-a37612047f12");
            await _personService.CreatePerson(new() { Name = "Ahmet", LastName = "Genç" }, "9e8ab117-c11f-4053-86ff-a37612047f13");
        }
    }
}
