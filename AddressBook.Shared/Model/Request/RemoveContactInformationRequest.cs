namespace AddressBook.Shared.Model.Request
{
    public class RemoveContactInformationRequest
    {
        public int ContactId { get; set; }
        public int ContactInformationId { get; set; }
    }
}
