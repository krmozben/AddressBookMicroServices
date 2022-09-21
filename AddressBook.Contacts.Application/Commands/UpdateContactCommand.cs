using MediatR;

namespace AddressBook.Contacts.Application.Commands
{
    public class UpdateContactCommand : IRequest
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }
    }
}
