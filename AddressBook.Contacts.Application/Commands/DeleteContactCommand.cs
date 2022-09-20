using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class DeleteContactCommand : IRequest
    {
        public int Id { get; set; }
    }
}
