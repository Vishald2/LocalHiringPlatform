using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LocalHiringPlatform.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? "";

            if (path.StartsWith("/swagger") ||
                path.StartsWith("/favicon") ||
                path.StartsWith("/health"))
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation(
                "Started {Method} {Path}",
                context.Request.Method,
                path);

            await _next(context);

            _logger.LogInformation(
                "Completed {Method} {Path} {StatusCode} {Duration}ms",
                context.Request.Method,
                path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}