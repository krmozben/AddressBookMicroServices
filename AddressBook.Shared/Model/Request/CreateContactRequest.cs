namespace AddressBook.Shared.Model.Request
{
    public class CreateContactRequest
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }
    }
}
