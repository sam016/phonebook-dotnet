using System;
using System.Threading.Tasks;
using FluentAssertions;
using Sam016.Phonebook.API.Models.Requests.Auth;
using Sam016.Phonebook.API.Tests.Factory;
using Sam016.Phonebook.Domain.Dtos;
using Xunit;

namespace Sam016.Phonebook.API.Tests.Controllers
{
    public class AuthControllerTest : BaseControllerTest, IClassFixture<MyWebApplicationFactory>
    {
        const string URL_LOGIN = "/api/auth/login";
        public AuthControllerTest(MyWebApplicationFactory factory) : base(factory)
        { }

        [Theory]
        [InlineData("a1@test.com", "password")]
        // [InlineData("a2@test.com", "password")]
        public async Task Test_CorrectCredentials_Should_LogInAsync(string email, string password)
        {
            var result = await PostAsync<AuthTokenDto>("", URL_LOGIN, new LoginRequest()
            {
                Email = email,
                Password = password,
            }, 200);

            result.Token.Should().NotBeEmpty();
            result.ExpiresAt.Should().BeAfter(DateTime.UtcNow.AddMinutes(59));
        }
    }
}
