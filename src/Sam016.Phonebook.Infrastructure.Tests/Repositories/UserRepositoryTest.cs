using Sam016.Phonebook.Infrastructure.Repositories;
using Sam016.Phonebook.Domain.Models;
using System.Threading.Tasks;
using System;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Sam016.Phonebook.Infrastructure.Tests.Repositories
{
    public class UserRepositoryTest : BaseRepositoryTest<User, UserRepository>, IClassFixture<DatabaseFixture>
    {
        public UserRepositoryTest(DatabaseFixture fixture) : base(fixture)
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
            // var repo = await NewRepositoryAsync();

            // var user = await repo.CreateAsync(new User()
            // {
            //     FirstName = firstName,
            //     LastName = lastName,
            //     Email = email,
            //     Password = password,
            // });

            await ExecuteInRepositoryAsync(async (UserRepository repo) =>
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

            // user.Id.Should().Be(1);
        }

        [Fact]
        public async Task TestGenericGetAllAsync()
        {
            await ExecuteInRepositoryAsync(async (UserRepository repo) =>
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

            // var repo = await NewRepositoryAsync();

            // var users = await repo.GetAllAsync();

            // users.Should().BeEquivalentTo(new User[]{
            //     new User{Id=1, FirstName="first-1", LastName= "last-1", Email="a1@test.com", Password = "password"},
            //     new User{Id=2, FirstName="first-2", LastName= "last-2", Email="a2@test.com", Password = "password"},
            //     new User{Id=3, FirstName="first-3", LastName= "last-3", Email="a3@test.com", Password = "password"},
            //     new User{Id=4, FirstName="first-4", LastName= "last-4", Email="a4@test.com", Password = "password"},
            //     new User{Id=5, FirstName="first-5", LastName= "last-5", Email="a5@test.com", Password = "password"},
            // });
        }

        [Theory]
        [InlineData(1, "first-1", "last-1", "a1@test.com", "password")]
        [InlineData(2, "first-2", "last-2", "a2@test.com", "password")]
        [InlineData(3, "first-3", "last-3", "a3@test.com", "password")]
        [InlineData(4, "first-4", "last-4", "a4@test.com", "password")]
        [InlineData(5, "first-5", "last-5", "a5@test.com", "password")]
        public async Task TestGenericGetByIdAsync(int id, string firstName, string lastName, string email, string password)
        {

            await ExecuteInRepositoryAsync(async (UserRepository repo) =>
            {
                var user = await repo.GetByIdAsync(id);

                user.FirstName.Should().Be(firstName);
                user.LastName.Should().Be(lastName);
                user.Email.Should().Be(email);
                user.Password.Should().Be(password);
            });

            // var repo = await NewRepositoryAsync();

            // var user = await repo.GetByIdAsync(id);

            // user.FirstName.Should().Be(firstName);
            // user.LastName.Should().Be(lastName);
            // user.Email.Should().Be(email);
            // user.Password.Should().Be(password);
        }

        protected override async Task AddTestDataAsync(DbContext context)
        {
            await context.AddRangeAsync(new User[]{
                new User{Id=1, FirstName="first-1", LastName= "last-1", Email="a1@test.com", Password = "password"},
                new User{Id=2, FirstName="first-2", LastName= "last-2", Email="a2@test.com", Password = "password"},
                new User{Id=3, FirstName="first-3", LastName= "last-3", Email="a3@test.com", Password = "password"},
                new User{Id=4, FirstName="first-4", LastName= "last-4", Email="a4@test.com", Password = "password"},
                new User{Id=5, FirstName="first-5", LastName= "last-5", Email="a5@test.com", Password = "password"},
            });
            await context.SaveChangesAsync();
        }

        protected override async Task<UserRepository> NewRepositoryAsync()
        {
            var context = NewContext();
            await AddTestDataAsync(context);
            return new UserRepository(Fixture.DbContext);
        }

        private async Task ExecuteInRepositoryAsync(Func<UserRepository, Task> func)
        {
            using (var context = NewContext())
            {
                await AddTestDataAsync(context);
                var repo = new UserRepository(Fixture.DbContext);
                await func(repo);
            }
        }
    }
}
