using AddressBook.Shared.Model.Request;

namespace AddressBook.Contacts.Application.Services.Implementation
{
    public interface IContactService
    {
        public Task CreateContact(CreateContactRequest request);
        public Task RemoveContact(int id);
        public Task UpdateContact(UpdateContactRequest request);
        public Task DeleteContact(int id);
        public Task AddContactInformation(AddContactInformationRequest request);
        public Task RemoveContactInformation(RemoveContactInformationRequest request);
    }
}
