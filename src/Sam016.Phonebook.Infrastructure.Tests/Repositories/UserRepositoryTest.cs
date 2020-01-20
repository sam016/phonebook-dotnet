using Sam016.Phonebook.Infrastructure.Repositories;
using Sam016.Phonebook.Domain.Models;
using System.Threading.Tasks;
using System;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Sam016.Phonebook.Infrastructure.Tests.Repositories
{
    public class UserRepositoryTest : BaseRepositoryTest<User, UserRepository>
    {
        public UserRepositoryTest() : base()
        {
        }

        [Theory]
        [InlineData("first-06", "last-06", "a06@test.com", "password")]
        [InlineData("first-07", "last-07", "a07@test.com", "password")]
        [InlineData("first-08", "last-08", "a08@test.com", "password")]
        [InlineData("first-09", "last-09", "a09@test.com", "password")]
        [InlineData("first-10", "last-10", "a10@test.com", "password")]
        public async Task TestGenericCreateAsync(string firstName, string lastName, string email, string password)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, UserRepository repo) =>
            {
                var user = await repo.CreateAsync(new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                });

                user.Id.Should().NotBe(0);
            });
        }

        [Fact]
        public async Task TestGenericGetAllAsync()
        {
            await ExecuteInRepositoryAsync(async (DbContext context, UserRepository repo) =>
            {
                var users = await repo.GetAllAsync();

                users.Should().BeEquivalentTo(new User[]{
                    new User{Id=1, FirstName="first-1", LastName= "last-1", Email="a1@test.com", Password = "password"},
                    new User{Id=2, FirstName="first-2", LastName= "last-2", Email="a2@test.com", Password = "password"},
                    new User{Id=3, FirstName="first-3", LastName= "last-3", Email="a3@test.com", Password = "password"},
                    new User{Id=4, FirstName="first-4", LastName= "last-4", Email="a4@test.com", Password = "password"},
                    new User{Id=5, FirstName="first-5", LastName= "last-5", Email="a5@test.com", Password = "password"},
                });
            });
        }

        [Theory]
        [InlineData(1, "first-1", "last-1", "a1@test.com", "password")]
        [InlineData(2, "first-2", "last-2", "a2@test.com", "password")]
        [InlineData(3, "first-3", "last-3", "a3@test.com", "password")]
        [InlineData(4, "first-4", "last-4", "a4@test.com", "password")]
        [InlineData(5, "first-5", "last-5", "a5@test.com", "password")]
        public async Task TestGenericGetByIdAsync(uint id, string firstName, string lastName, string email, string password)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, UserRepository repo) =>
            {
                var user = await repo.GetByIdAsync(id);

                user.FirstName.Should().Be(firstName);
                user.LastName.Should().Be(lastName);
                user.Email.Should().Be(email);
                user.Password.Should().Be(password);
            });
        }

        [Theory]
        [InlineData(1, "new-first-1", "new-last-1", "new-a1@test.com", "new-password")]
        [InlineData(2, "new-first-2", "new-last-2", "new-a2@test.com", "new-password")]
        [InlineData(3, "new-first-3", "new-last-3", "new-a3@test.com", "new-password")]
        [InlineData(4, "new-first-4", "new-last-4", "new-a4@test.com", "new-password")]
        [InlineData(5, "new-first-5", "new-last-5", "new-a5@test.com", "new-password")]
        public async Task TestGenericUpdateAsync(uint id, string firstName, string lastName, string email, string password)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, UserRepository repo) =>
            {
                await repo.UpdateAsync(new User()
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                });

                var updatedUser = await context.Set<User>().FindAsync(id);

                updatedUser.FirstName.Should().Be(firstName);
                updatedUser.LastName.Should().Be(lastName);
                updatedUser.Email.Should().Be(email);
                updatedUser.Password.Should().Be(password);
            });
        }

        [Theory]
        // [InlineData(1)] // Commented as its a foreign key to some phonebooks
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task TestGenericDeleteByIdAsync(uint id)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, UserRepository repo) =>
            {
                await repo.DeleteAsync(id);

                var user = await context.Set<User>().FindAsync(id);

                user.Should().BeNull();
            });
        }

        protected override async Task ExecuteInRepositoryAsync(Func<DbContext, UserRepository, Task> func)
        {
            using (var context = NewDbContext())
            {
                await AddTestDataAsync(context);
                var repo = new UserRepository(context);
                await func(context, repo);
            }
        }
    }
}
