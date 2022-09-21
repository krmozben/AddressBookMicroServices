using MongoDB.Driver;


namespace AddressBook.Person.Data.Data.Implementation
{
    public interface IMongoDbDataContext
    {
        IMongoCollection<Model.Person> PersonModel { get; }
    }
}
