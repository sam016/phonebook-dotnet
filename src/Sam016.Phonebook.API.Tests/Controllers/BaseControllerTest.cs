using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Sam016.Phonebook.API.Tests.Extensions;
using Sam016.Phonebook.API.Tests.Factory;
using Xunit;

namespace Sam016.Phonebook.API.Tests.Controllers
{
    public abstract class BaseControllerTest
    {
        protected readonly HttpClient Client;
        protected readonly MyWebApplicationFactory Factory;

        public BaseControllerTest(MyWebApplicationFactory factory)
        {
            Factory = factory;
            Client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((_, conf) =>
                {
                    var projectDir = Directory.GetCurrentDirectory();
                    var configPath = Path.Combine(projectDir, "appsettings.Test.json");

                    conf.AddJsonFile(configPath);
                });
                builder.ConfigureServices(services =>
                {
                    Console.WriteLine("\t--Configring services test-location-02");
                    services.ConfigureInMemoryDB();
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        // private Microsoft.Extensions.Configuration.IConfiguration GetConfiguration()
        // {
        //     var config = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.test.json")
        //     //    .AddJsonFile("appsettings.development.json")
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //     return config;
        // }

        protected async Task<string> GetTokenAsync(string email, string password)
        {
            var dto = await PostAsync<Domain.Dtos.AuthTokenDto>(null, "/auth/login", new Models.Requests.Auth.LoginRequest
            {
                Email = email,
                Password = password,
            });

            return dto.Token;
        }

        protected async Task<O> PostAsync<O>(string authToken, string url, object request, int statusCode = 200)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                Client.DefaultRequestHeaders.Remove("Authorization");
            }
            else
            {
                Client.DefaultRequestHeaders.Add("Authorization", "bearer " + authToken);
            }

            var response = await Client.PostAsJsonAsync(url, request);

            ((int)response.StatusCode).Should().Be(statusCode);

            if (response.IsSuccessStatusCode)
            {
                return await response.GetValueAsync<O>();
            }

            return default;
        }

        protected async Task<O> GetAsync<O>(string authToken, string url, int statusCode = 200)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                Client.DefaultRequestHeaders.Remove("Authorization");
            }
            else
            {
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);
            }

            var response = await Client.GetAsync(url);

            ((int)response.StatusCode).Should().Be(statusCode);

            if (response.IsSuccessStatusCode)
            {
                return await response.GetValueAsync<O>();
            }

            return default;
        }
    }
}
