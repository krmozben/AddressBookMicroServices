using AddressBook.Person.Data.Data.Implementation;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace AddressBook.Person.Data.Data
{

    public class MongoDbDataContext : IMongoDbDataContext
    {
        internal readonly IMongoDatabase _mongoDatabase;

        public MongoDbDataContext(IMongoClient mongoClient, IConfiguration config)
        {
            // Set up MongoDB conventions
            var pack = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };
            ConventionRegistry.Register("EnumStringConvention", pack, t => true);

            var dbName = config.GetSection("MongoDbName").Value;
            if (!mongoClient.ListDatabaseNames().ToList().Contains(dbName)) throw new System.Exception($"{dbName} database does not exist!");

            _mongoDatabase = mongoClient.GetDatabase(dbName);

            ///TO-DO: Initial collection and index checks/creations
        }

        public IMongoCollection<Model.Person> PersonModel => _mongoDatabase.GetCollection<Model.Person>("Person");
    }
}
