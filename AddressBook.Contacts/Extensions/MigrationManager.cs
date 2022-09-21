﻿using AddressBook.Contacts.Application.AddressBookContextSeed;
using AddressBook.Contacts.Infrastructure;
using Microsoft.Data.SqlClient;

namespace AddressBook.Contacts.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host, ILogger logger, IConfiguration configuration)
        {
            CreateDatabase(configuration, logger);

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var addressBookDbContext = scope.ServiceProvider.GetRequiredService<AddressBookContextSeed>();

                    addressBookDbContext.SeedAsync().Wait();
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex.Message);
                }

                return host;
            }
        }

        private static void CreateDatabase(IConfiguration configuration, ILogger logger)
        {
            var retryCount = 0;
            var maximumRetryCount = 5;
            var done = false;
            var retryDelayInSeconds = 15;

            while (retryCount < maximumRetryCount && !done)
            {
                retryCount++;

                try
                {
                    string defaultDbName = string.Empty;
                    SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
                    defaultDbName = sqlConnection.Database;
                    sqlConnection.ConnectionString = sqlConnection.ConnectionString.Replace(defaultDbName, "master");
                    sqlConnection.Open();
                    using SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    {
                        sqlCommand.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{defaultDbName}') BEGIN CREATE DATABASE {defaultDbName} END";
                        sqlCommand.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    done = true;
                }
                catch (Exception e)
                {
                    logger.LogCritical($"{e.Message}; TryCount = {retryCount}");
                }

                if (!done)
                {
                    Thread.Sleep(retryDelayInSeconds * 1000);
                }
            }
        }
    }
}
