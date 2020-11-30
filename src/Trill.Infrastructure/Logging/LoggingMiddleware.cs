using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Trill.Infrastructure.Logging
{
    internal class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var id = context.TraceIdentifier;
            _logger.LogInformation($"Starting the request, path: '{context.Request.Path}', ID: '{id}'...");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next(context);
            stopWatch.Stop();
            _logger.LogInformation($"Finished the request, path: '{context.Request.Path}', ID: '{id}', " +
                                   $"status code: {context.Response.StatusCode}, time: {stopWatch.ElapsedMilliseconds} ms.");
        }
    }
}