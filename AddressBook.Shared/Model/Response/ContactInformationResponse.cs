namespace AddressBook.Shared.Model.Response
{
    public class ContactInformationResponse
    {
        public ContactInformationResponse()
        {
            ContactInformation = new List<InformationResponse>();
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Firm { get; set; }
        public List<InformationResponse> ContactInformation { get; set; }
    }
}
