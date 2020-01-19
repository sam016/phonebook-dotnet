using System;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Sam016.Phonebook.Infrastructure.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DbContext DbContext { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<PhonebookContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            DbContext = new PhonebookContext(options);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
