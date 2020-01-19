using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Sam016.Phonebook.API.Tests.Extensions;

namespace Sam016.Phonebook.API.Tests.Factory
{
    public class MyWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, conf) =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.Test.json");

                conf.AddJsonFile(configPath);
            });
            builder.ConfigureServices(services =>
            {
                Console.WriteLine("\t--Configring services test-location-01");
                services.ConfigureInMemoryDB();
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
    }
}
