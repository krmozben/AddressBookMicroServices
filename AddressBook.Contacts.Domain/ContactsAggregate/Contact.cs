using AddressBook.Contacts.DomainCore;

namespace AddressBook.Contacts.Domain.ContactsAggregate
{
    public class Contact : Entity, IAggregateRoot
    {
        public string Uuid { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public Firm Firm { get; private set; }
        public List<ContactInformation> ContactInformation { get; private set; }

        public Contact()
        {

        }

        public Contact(string name, string lastName, string uuid, Firm firm)
        {
            ContactInformation = new List<ContactInformation>();
            Name = name;
            LastName = lastName;
            Firm = firm;
            Uuid = uuid;
        }

        public void UpdateContact(string name, string lastName, string firm)
        {
            Name = name;
            LastName = lastName;
            Firm = new Firm(firm);
        }

        public void AddContactInformation(ContactInformation contactInformation)
        {
            ContactInformation.Add(contactInformation);
        }

        public object ToList()
        {
            throw new NotImplementedException();
        }

        public void RemoveContactInformation(int contactInformationId)
        {
            var contactInformation = ContactInformation.Where(x => x.Id == contactInformationId).First();

            ContactInformation.Remove(contactInformation);
        }

        public void SetActive() => IsActive = true;

        public void SetPassife() => IsActive = false;
    }
}
