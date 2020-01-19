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
        protected readonly DatabaseFixture Fixture;

        protected abstract Task AddTestDataAsync(DbContext context);
        protected abstract Task<TRepository> NewRepositoryAsync();

        public BaseRepositoryTest(DatabaseFixture fixture)
        {
            Fixture = fixture;
            // AddTestDataAsync(Fixture.DbContext);
        }

        public DbContext NewContext()
        {
            var random = new Random();
            var options = new DbContextOptionsBuilder<PhonebookContext>()
               .UseInMemoryDatabase(databaseName: "DB-" + random.Next())
               .Options;

            return new PhonebookContext(options);
        }
    }
}
