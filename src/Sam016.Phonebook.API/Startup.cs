using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sam016.Phonebook.API.Extensions;
using Sam016.Phonebook.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Sam016.Phonebook.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("\t--Configring services location-01");

            services.AddControllers();
            services.ConfigureMediatR();
            services.ConfigureSwagger();
            services.ConfigureRepositories();
            services.AddAuthentication(Configuration);
            services.AddAutoMapper(typeof(Startup), typeof(Domain.Models.BaseModel));

            services.AddDbContext<DbContext, PhonebookContext>(options =>
            {
                Console.WriteLine("\t--Configring DBContext location-01");

                var dbType = Configuration.GetValue<string>("Database:Type");

                if (dbType == "MySql")
                {
                    var dbName = Configuration.GetValue<string>("Database:DbName");
                    var host = Configuration.GetValue<string>("Database:Host");
                    var user = Configuration.GetValue<string>("Database:User");
                    var pass = Configuration.GetValue<string>("Database:Pass");

                    options.UseMySql($"server={host};database={dbName};user={user};password={pass};Convert Zero Datetime=True;", x => x.ServerVersion("8.0.15-mysql"));
                }
                else
                {
                    var random = new Random();

                    options.UseInMemoryDatabase("DB-" + random.Next());
                    options.EnableSensitiveDataLogging(true);
                }
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStopwatchMiddleware();

            app.UseHttpsRedirection();

            app.InjectSwagger();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
