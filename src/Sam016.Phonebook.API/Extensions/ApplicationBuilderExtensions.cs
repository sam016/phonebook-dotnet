using System;
using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Sam016.Phonebook.API.Middlewares;

namespace Sam016.Phonebook.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder InjectSwagger(this IApplicationBuilder builder)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            builder.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            return builder;
        }

        public static IApplicationBuilder UseStopwatchMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StopwatchMiddleware>();
        }
    }
}
