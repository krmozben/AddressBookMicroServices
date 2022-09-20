using AddressBook.Contacts.DomainCore;
using AddressBook.Shared.Enums;

namespace AddressBook.Contacts.Domain.ContactsAggregate
{
    public class ContactInformation : Entity
    {
        public InformationType Type { get; private set; }
        public string Content { get; private set; }

        public ContactInformation()
        {

        }

        public ContactInformation(InformationType type, string content)
        {
            Type = type;
            Content = content;
        }
    }
}
