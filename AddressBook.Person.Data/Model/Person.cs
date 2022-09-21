using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AddressBook.Person.Data.Model
{
    public class Person
    {
        [BsonId]
        [BsonRequired]
        public string Uuid { get; set; }
        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public string LastName { get; set; }
    }
}
