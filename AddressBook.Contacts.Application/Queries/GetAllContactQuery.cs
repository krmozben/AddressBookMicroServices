using AddressBook.Contacts.Domain.ContactsAggregate;
using MediatR;

namespace AddressBook.Contacts.Application.Queries
{
    public class GetAllContactQuery : IRequest<List<Contact>>
    {
    }
}
