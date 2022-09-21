using AddressBook.Contacts.Domain.ContactsAggregate;
using MediatR;

namespace AddressBook.Contacts.Application.Queries
{
    public class GetContactQuery : IRequest<Contact>
    {
        public string Uuid { get; set; }
    }
}
