using AddressBook.Person.Service.Model.Request;
using AddressBook.Person.Service.Model.Response;

namespace AddressBook.Person.Service.Services.Implementation
{
    public interface IPersonService
    {
        public Task CreatePerson(CreatePersonRequest request, string uuid = null);
        public Task<List<PersonListResponse>> GetPersons();
    }
}
