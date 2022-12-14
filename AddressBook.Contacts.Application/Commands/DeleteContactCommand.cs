using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class DeleteContactCommand : IRequest
    {
        public string Uuid { get; set; }
    }
}
