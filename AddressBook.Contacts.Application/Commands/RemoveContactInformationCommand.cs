using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class RemoveContactInformationCommand : IRequest
    {
        public string Uuid { get; set; }
        public int ContactInformationId { get; set; }
    }
}
