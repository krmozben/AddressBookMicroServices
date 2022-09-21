using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Shared.Model.Request;
using AddressBook.Shared.Model.Response;

namespace AddressBook.Contacts.Application.Services.Implementation
{
    public interface IContactService
    {
        public Task CreateContact(CreateContactRequest request);
        public Task RemoveContact(string uuid);
        public Task UpdateContact(UpdateContactRequest request);
        public Task DeleteContact(string uuid);
        public Task AddContactInformation(AddContactInformationRequest request);
        public Task RemoveContactInformation(RemoveContactInformationRequest request);
        public Task<List<ContactResponse>> GetAllContacts();
        public Task SetAllContactsOnRedis(List<Contact> contacts);
        public Task<ContactInformationResponse> GetContact(string Uuid);
        public Task<List<LocationReportResponse>> GetLocationReport();
    }
}
