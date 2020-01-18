using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Sam016.Phonebook.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureMediatR(this IServiceCollection service)
        {
            service.AddMediatR(typeof(Program));
        }
    }
}
