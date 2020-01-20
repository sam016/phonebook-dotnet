using Sam016.Phonebook.Infrastructure.Repositories;
using Sam016.Phonebook.Domain.Models;
using System.Threading.Tasks;
using System;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PhonebookModel = Sam016.Phonebook.Domain.Models.Phonebook;

namespace Sam016.Phonebook.Infrastructure.Tests.Repositories
{
    public class PhonebookTest : BaseRepositoryTest<PhonebookModel, PhonebookRepository>
    {
        public PhonebookTest() : base()
        {
        }

        [Theory]
        [InlineData("name-06", 1)]
        [InlineData("name-07", 1)]
        [InlineData("name-08", 1)]
        [InlineData("name-09", 1)]
        [InlineData("name-10", 1)]
        public async Task TestGenericCreateAsync(string name, uint userId)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhonebookRepository repo) =>
            {
                var phonebook = await repo.CreateAsync(new PhonebookModel()
                {
                    Name = name,
                    UserId = userId,
                });

                phonebook.Id.Should().NotBe(0);
            });
        }

        [Fact]
        public async Task TestGenericGetAllAsync()
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhonebookRepository repo) =>
            {
                var phonebooks = await repo.GetAllAsync();

                phonebooks.Should().BeEquivalentTo(new PhonebookModel[]{
                    new PhonebookModel{Id=1, Name="name-1", UserId=1},
                    new PhonebookModel{Id=2, Name="name-2", UserId=1},
                    new PhonebookModel{Id=3, Name="name-3", UserId=1},
                    new PhonebookModel{Id=4, Name="name-4", UserId=1},
                    new PhonebookModel{Id=5, Name="name-5", UserId=1},
                });
            });
        }

        [Theory]
        [InlineData(1, "name-1", 1)]
        [InlineData(2, "name-2", 1)]
        [InlineData(3, "name-3", 1)]
        [InlineData(4, "name-4", 1)]
        [InlineData(5, "name-5", 1)]
        public async Task TestGenericGetByIdAsync(uint id, string name, uint userId)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhonebookRepository repo) =>
            {
                var phonebook = await repo.GetByIdAsync(id);

                phonebook.Name.Should().Be(name);
                phonebook.UserId.Should().Be(userId);
            });
        }

        [Theory]
        [InlineData(1, "new-name-1", 1)]
        [InlineData(2, "new-name-2", 1)]
        [InlineData(3, "new-name-3", 1)]
        [InlineData(4, "new-name-4", 1)]
        [InlineData(5, "new-name-5", 1)]
        public async Task TestGenericUpdateAsync(uint id, string name, uint userId)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhonebookRepository repo) =>
            {
                await repo.UpdateAsync(new PhonebookModel()
                {
                    Id = id,
                    Name = name,
                    UserId = userId,
                });

                var updatedPhonebook = await context.Set<PhonebookModel>().FindAsync(id);

                updatedPhonebook.Name.Should().Be(name);
                updatedPhonebook.UserId.Should().Be(userId);
            });
        }

        [Theory]
        // [InlineData(1)] // Commented as its a foreign key to some phone entries
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task TestGenericDeleteByIdAsync(uint id)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhonebookRepository repo) =>
            {
                await repo.DeleteAsync(id);

                var phonebook = await context.Set<PhonebookModel>().FindAsync(id);

                phonebook.Should().BeNull();
            });
        }

        protected override async Task ExecuteInRepositoryAsync(Func<DbContext, PhonebookRepository, Task> func)
        {
            using (var context = NewDbContext())
            {
                await AddTestDataAsync(context);
                var repo = new PhonebookRepository(context);
                await func(context, repo);
            }
        }
    }
}
