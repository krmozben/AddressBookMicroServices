using Microsoft.EntityFrameworkCore;

namespace AddressBook.Contacts.Domain.Sp
{
    [Keyless]
    public class LocationReport
    {
        public string Location { get; set; }
        public int Count { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
