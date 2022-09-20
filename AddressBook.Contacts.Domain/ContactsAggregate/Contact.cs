using AddressBook.Contacts.DomainCore;

namespace AddressBook.Contacts.Domain.ContactsAggregate
{
    public class Contact : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public Firm Firm { get; private set; }
        public List<ContactInformation> ContactInformation { get; private set; }

        public Contact()
        {

        }

        public Contact(string name, string lastName, Firm firm)
        {
            ContactInformation = new List<ContactInformation>();
            Name = name;
            LastName = lastName;
            Firm = firm;
        }

        public void AddContactInformation(ContactInformation contactInformation)
        {
            ContactInformation.Add(contactInformation);
        }
    }
}
