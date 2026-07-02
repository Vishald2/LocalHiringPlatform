using LocalHiringPlatform.Domain.Exceptions;

namespace LocalHiringPlatform.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware>
    _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message = ex.Message
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception occurred. Request: {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        Message = "An unexpected error occurred."
                    });
            }
        }
    }
}
