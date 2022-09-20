using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class RemoveContactCommand : IRequest
    {
        public int Id { get; set; }
    }
}
