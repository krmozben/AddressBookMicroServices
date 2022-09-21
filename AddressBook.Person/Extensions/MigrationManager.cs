using AddressBook.Person.Service.PersonFeed;

namespace AddressBook.Person.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host, ILogger logger, IConfiguration configuration)
        {

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var personFeed = scope.ServiceProvider.GetRequiredService<PersonFeed>();

                    personFeed.SeedAsync().Wait();
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex.Message);
                }

                return host;
            }
        }
    }
}
