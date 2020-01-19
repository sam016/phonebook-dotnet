using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sam016.Phonebook.API.Tests.Controllers;
using Sam016.Phonebook.API.Tests.Utilities;
using Sam016.Phonebook.Infrastructure;

namespace Sam016.Phonebook.API.Tests.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureInMemoryDB(this IServiceCollection services)
        {
            Console.WriteLine("\t--Configring DBContext-InMemory test-location-01");

            // Remove the app's DatabaseContext registration.
            var descriptors = services.Where(d => d.ServiceType == typeof(DbContext)
            || d.ServiceType == typeof(DbContextOptions<PhonebookContext>)).ToList();

            foreach (var descriptor in descriptors)
            {
                Console.WriteLine("Removing `{0}` => `{1}`", descriptor.ServiceType?.FullName, descriptor.ImplementationType?.FullName);
                services.Remove(descriptor);
            }

            var random = new Random();

            // Add DatabaseContext using an in-memory database for testing.
            services.AddDbContext<DbContext, PhonebookContext>((_, options) =>
            {
                options.UseInMemoryDatabase("DB-" + random.Next());
                options.EnableSensitiveDataLogging(true);
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (DatabaseContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetService<DbContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<BaseControllerTest>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    DbUtilities.InitializeDbForTests(db);

                    Console.WriteLine("\t--Users count just after seed {0}", db.Set<Domain.Models.User>().ToList().Count);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
                }
            }
        }
    }
}
