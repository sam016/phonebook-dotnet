using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sam016.Phonebook.Infrastructure.Tests.Repositories
{
    public abstract class BaseRepositoryTest<TModel, TRepository>
        where TModel : Sam016.Phonebook.Domain.Models.BaseModel, new()
        where TRepository : Sam016.Phonebook.Infrastructure.Repositories.BaseRepository<TModel>
    {
        protected abstract Task ExecuteInRepositoryAsync(Func<DbContext, TRepository, Task> func);

        public BaseRepositoryTest()
        {
        }

        public DbContext NewDbContext()
        {
            var random = new Random();
            var options = new DbContextOptionsBuilder<PhonebookContext>()
               .UseInMemoryDatabase(databaseName: "DB-" + random.Next())
               .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
               .EnableSensitiveDataLogging()
               .Options;

            return new PhonebookContext(options);
        }

        protected virtual async Task AddTestDataAsync(DbContext context)
        {
            await context.AddRangeAsync(new Domain.Models.User[]{
                new Domain.Models.User{Id=1, FirstName="first-1", LastName= "last-1", Email="a1@test.com", Password = "password"},
                new Domain.Models.User{Id=2, FirstName="first-2", LastName= "last-2", Email="a2@test.com", Password = "password"},
                new Domain.Models.User{Id=3, FirstName="first-3", LastName= "last-3", Email="a3@test.com", Password = "password"},
                new Domain.Models.User{Id=4, FirstName="first-4", LastName= "last-4", Email="a4@test.com", Password = "password"},
                new Domain.Models.User{Id=5, FirstName="first-5", LastName= "last-5", Email="a5@test.com", Password = "password"},
            });

            await context.AddRangeAsync(new Domain.Models.Phonebook[]{
                new Domain.Models.Phonebook{Id=1, Name="name-1", UserId=1},
                new Domain.Models.Phonebook{Id=2, Name="name-2", UserId=1},
                new Domain.Models.Phonebook{Id=3, Name="name-3", UserId=1},
                new Domain.Models.Phonebook{Id=4, Name="name-4", UserId=1},
                new Domain.Models.Phonebook{Id=5, Name="name-5", UserId=1},
            });

            await context.AddRangeAsync(new Domain.Models.PhoneEntry[]{
                new Domain.Models.PhoneEntry{Id=01,FirstName= "first-01",LastName="last-01", Country="+91", Phone="9875642301", OrganizationName="org-name-01", Address="address-01", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=02,FirstName= "first-02",LastName="last-02", Country="+91", Phone="9875642302", OrganizationName="org-name-02", Address="address-02", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=03,FirstName= "first-03",LastName="last-03", Country="+91", Phone="9875642303", OrganizationName="org-name-03", Address="address-03", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=04,FirstName= "first-04",LastName="last-04", Country="+91", Phone="9875642304", OrganizationName="org-name-04", Address="address-04", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=05,FirstName= "first-05",LastName="last-05", Country="+91", Phone="9875642305", OrganizationName="org-name-05", Address="address-05", PhonebookId=1},
            });

            await context.SaveChangesAsync();
        }
    }
}
