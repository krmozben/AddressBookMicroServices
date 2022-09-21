namespace AddressBook.Shared.Model.Request
{
    public class RemoveContactInformationRequest
    {
        public string Uuid { get; set; }
        public int ContactInformationId { get; set; }
    }
}
