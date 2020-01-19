using Sam016.Phonebook.Infrastructure.Repositories;
using Sam016.Phonebook.Domain.Models;
using System.Threading.Tasks;
using System;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Sam016.Phonebook.Infrastructure.Tests.Repositories
{
    public class PhoneEntryRepositoryTest : BaseRepositoryTest<PhoneEntry, PhoneEntryRepository>
    {
        public PhoneEntryRepositoryTest() : base()
        {
        }

        [Theory]
        [InlineData("first-06", "last-06", "+91", "9875642306", "org-name-06", "address-06", 1)]
        [InlineData("first-07", "last-07", "+91", "9875642307", "org-name-07", "address-07", 1)]
        [InlineData("first-08", "last-08", "+91", "9875642308", "org-name-08", "address-08", 1)]
        [InlineData("first-09", "last-09", "+91", "9875642309", "org-name-09", "address-09", 1)]
        [InlineData("first-10", "last-10", "+91", "9875642310", "org-name-10", "address-10", 1)]
        public async Task TestGenericCreateAsync(string FirstName, string LastName, string Country, string Phone, string OrganizationName, string Address, int PhonebookId)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhoneEntryRepository repo) =>
            {
                var phoneEntry = await repo.CreateAsync(new PhoneEntry()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Country = Country,
                    Phone = Phone,
                    OrganizationName = OrganizationName,
                    Address = Address,
                    PhonebookId = PhonebookId,
                });

                phoneEntry.Id.Should().NotBe(0);
            });
        }

        [Fact]
        public async Task TestGenericGetAllAsync()
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhoneEntryRepository repo) =>
            {
                var users = await repo.GetAllAsync();

                users.Should().BeEquivalentTo(new PhoneEntry[]{
                new Domain.Models.PhoneEntry{Id=01,FirstName= "first-01",LastName="last-01", Country="+91", Phone="9875642301", OrganizationName="org-name-01", Address="address-01", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=02,FirstName= "first-02",LastName="last-02", Country="+91", Phone="9875642302", OrganizationName="org-name-02", Address="address-02", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=03,FirstName= "first-03",LastName="last-03", Country="+91", Phone="9875642303", OrganizationName="org-name-03", Address="address-03", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=04,FirstName= "first-04",LastName="last-04", Country="+91", Phone="9875642304", OrganizationName="org-name-04", Address="address-04", PhonebookId=1},
                new Domain.Models.PhoneEntry{Id=05,FirstName= "first-05",LastName="last-05", Country="+91", Phone="9875642305", OrganizationName="org-name-05", Address="address-05", PhonebookId=1},
                });
            });
        }

        [Theory]
        [InlineData(01, "first-01", "last-01", "+91", "9875642301", "org-name-01", "address-01", 1)]
        [InlineData(02, "first-02", "last-02", "+91", "9875642302", "org-name-02", "address-02", 1)]
        [InlineData(03, "first-03", "last-03", "+91", "9875642303", "org-name-03", "address-03", 1)]
        [InlineData(04, "first-04", "last-04", "+91", "9875642304", "org-name-04", "address-04", 1)]
        [InlineData(05, "first-05", "last-05", "+91", "9875642305", "org-name-05", "address-05", 1)]
        public async Task TestGenericGetByIdAsync(int id, string FirstName, string LastName, string Country, string Phone, string OrganizationName, string Address, int PhonebookId)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhoneEntryRepository repo) =>
            {
                var phoneEntry = await repo.GetByIdAsync(id);

                FirstName.Should().Be(FirstName);
                LastName.Should().Be(LastName);
                Country.Should().Be(Country);
                Phone.Should().Be(Phone);
                OrganizationName.Should().Be(OrganizationName);
                Address.Should().Be(Address);
                PhonebookId.Should().Be(PhonebookId);
            });
        }

        [Theory]
        [InlineData(01, "new-first-01", "new-last-01", "+1", "7875642301", "new-org-name-01", "new-address-01", 1)]
        [InlineData(02, "new-first-02", "new-last-02", "+1", "7875642302", "new-org-name-02", "new-address-02", 1)]
        [InlineData(03, "new-first-03", "new-last-03", "+1", "7875642303", "new-org-name-03", "new-address-03", 1)]
        [InlineData(04, "new-first-04", "new-last-04", "+1", "7875642304", "new-org-name-04", "new-address-04", 1)]
        [InlineData(05, "new-first-05", "new-last-05", "+1", "7875642305", "new-org-name-05", "new-address-05", 1)]
        public async Task TestGenericUpdateAsync(int id, string FirstName, string LastName, string Country, string Phone, string OrganizationName, string Address, int PhonebookId)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhoneEntryRepository repo) =>
            {
                await repo.UpdateAsync(new PhoneEntry()
                {
                    Id = id,
                    FirstName = FirstName,
                    LastName = LastName,
                    Country = Country,
                    Phone = Phone,
                    OrganizationName = OrganizationName,
                    Address = Address,
                    PhonebookId = PhonebookId,
                });

                var updatedPhoneEntry = await context.Set<PhoneEntry>().FindAsync(id);

                updatedPhoneEntry.FirstName.Should().Be(FirstName);
                updatedPhoneEntry.LastName.Should().Be(LastName);
                updatedPhoneEntry.Country.Should().Be(Country);
                updatedPhoneEntry.Phone.Should().Be(Phone);
                updatedPhoneEntry.OrganizationName.Should().Be(OrganizationName);
                updatedPhoneEntry.Address.Should().Be(Address);
                updatedPhoneEntry.PhonebookId.Should().Be(PhonebookId);
            });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task TestGenericDeleteByIdAsync(int id)
        {
            await ExecuteInRepositoryAsync(async (DbContext context, PhoneEntryRepository repo) =>
            {
                await repo.DeleteAsync(id);

                var phoneEntry = await context.Set<PhoneEntry>().FindAsync(id);

                phoneEntry.Should().BeNull();
            });
        }

        protected override async Task ExecuteInRepositoryAsync(Func<DbContext, PhoneEntryRepository, Task> func)
        {
            using (var context = NewDbContext())
            {
                await AddTestDataAsync(context);
                var repo = new PhoneEntryRepository(context);
                await func(context, repo);
            }
        }
    }
}
