using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class RemoveContactCommand : IRequest
    {
        public string Uuid { get; set; }
    }
}
