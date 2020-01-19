using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sam016.Phonebook.API.Middlewares
{
    public class StopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public StopwatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            //To add Headers AFTER everything you need to do this
            context.Response.OnStarting(state =>
            {
                // Stop timing.
                stopwatch.Stop();

                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("x-time-elapsed", new[] { stopwatch.Elapsed.ToString() });

                return Task.CompletedTask;
            }, context);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
