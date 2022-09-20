using AddressBook.Contacts.DomainCore;

namespace AddressBook.Contacts.Domain.ContactsAggregate
{
    public class Firm : ValueObject
    {
        public string? Name { get; private set; }

        public Firm()
        {

        }

        public Firm(string name)
        {
            Name = name;
        }
    }
}
