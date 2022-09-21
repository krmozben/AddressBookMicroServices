using AddressBook.Contacts.Domain.ContactsAggregate;
using MediatR;

namespace AddressBook.Contacts.Application.Queries
{
    public class GetContactQuery : IRequest<Contact>
    {
        public int Id { get; set; }
    }
}
