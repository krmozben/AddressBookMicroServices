using AddressBook.Contacts.Domain.ContactsAggregate;
using AddressBook.Contacts.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace AddressBook.Contacts.Application.AddressBookContextSeed
{
    public class AddressBookContextSeed
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly ILogger<AddressBookContextSeed> _logger;

        public AddressBookContextSeed(IRepository<Contact> contactRepository, ILogger<AddressBookContextSeed> logger)
        {
            _contactRepository = contactRepository;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _contactRepository.MigrateDatabaseAsync();

            var anyContact = await _contactRepository.GetListAsync(x => true);

            if (anyContact.ToList().Any())
                return;

            await _contactRepository.AddRangeAsync(GetSeedData());

            _logger.LogInformation(new EventId(1234), $"The initial data for the business logic is created.");
        }

        private List<Contact> GetSeedData()
        {
            Contact c1 = new("Kerim", "Özben", "9e8ab117-c11f-4053-86ff-a37612047f11", new Firm("Microsoft"));
            c1.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.Location,"Türkiye"));
            c1.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.PhoneNumber,"054151549"));
            c1.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.EmailAdress,"ozbenkerim@gmail.com"));

            Contact c2 = new("Halil", "Tayfur", "9e8ab117-c11f-4053-86ff-a37612047f12", new Firm("Apple"));
            c2.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.Location, "Türkiye"));
            c2.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.PhoneNumber, "054654456"));
            c2.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.PhoneNumber, "054648767"));

            Contact c3 = new("Ahmet", "Genç", "9e8ab117-c11f-4053-86ff-a37612047f13", new Firm("Microsoft"));
            c3.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.Location, "Hollanda"));
            c3.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.PhoneNumber, "065464793"));
            c3.AddContactInformation(new ContactInformation(Shared.Enums.InformationType.PhoneNumber, "065167829"));


            var contactList = new List<Contact>()
            {
                c1,c2, c3
            };

            contactList.AddRange(contactList);

            return contactList;
        }
    }
}
