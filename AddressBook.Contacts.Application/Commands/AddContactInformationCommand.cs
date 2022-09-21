using AddressBook.Shared.Enums;
using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class AddContactInformationCommand : IRequest
    {
        public string Uuid { get; set; }
        public InformationType Type { get; set; }
        public string Content { get; set; }
    }  }
