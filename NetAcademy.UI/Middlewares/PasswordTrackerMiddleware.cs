using Microsoft.Extensions.Logging;
using NetAcademy.UI.Middlewares;

namespace NetAcademy.UI.Middlewares
{
    public class PasswordTrackerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PasswordTrackerMiddleware> _logger;

        public PasswordTrackerMiddleware(ILoggerFactory loggerFactory,
            RequestDelegate next)
        {

            _next = next;
            _logger = loggerFactory?.CreateLogger<PasswordTrackerMiddleware>()
                      ?? throw new ArgumentException(nameof(_logger));
        }

        public async Task Invoke(HttpContext context)
        {
            // Code logic here'
            var password = context.Request.Query["password"].ToArray();
            if (password.Any())
            {
                _logger.LogCritical(password[0]);
            }
            await _next(context);
        }
    }
}

public static class PasswordTrackerMiddlewareExtensions
{
    public static IApplicationBuilder UsePasswordTracker(this IApplicationBuilder app)
    {
        return app.UseMiddleware<PasswordTrackerMiddleware>();
    }
}