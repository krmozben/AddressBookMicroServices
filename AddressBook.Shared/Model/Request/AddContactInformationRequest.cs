using AddressBook.Shared.Enums;

namespace AddressBook.Shared.Model.Request
{
    public class AddContactInformationRequest
    {
        public int ContactId { get; set; }
        public InformationType Type { get; set; }
        public string Content { get; set; }
    }  
}
