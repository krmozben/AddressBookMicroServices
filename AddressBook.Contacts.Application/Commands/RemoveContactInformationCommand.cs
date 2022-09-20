using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class RemoveContactInformationCommand : IRequest
    {
        public int ContactId { get; set; }
        public int ContactInformationId { get; set; }
    }
}
