using AddressBook.Shared.Enums;

namespace AddressBook.Shared.Model.Request
{
    public class AddContactInformationRequest
    {
        public string Uuid { get; set; }
        public InformationType Type { get; set; }
        public string Content { get; set; }
    }  
}
