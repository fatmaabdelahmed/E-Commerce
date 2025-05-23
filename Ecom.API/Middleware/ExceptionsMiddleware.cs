using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _ratelimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionsMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _environment = environment;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurity(context);

                if (!IsRequestAllowed(context))
                {
                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                        context.Response.ContentType = "application/json";

                        var response = new ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");
                        await context.Response.WriteAsJsonAsync(response);
                    }

                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var response = _environment.IsDevelopment()
                        ? new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                        : new ApiExceptions((int)HttpStatusCode.InternalServerError, "An error occurred");

                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                }
            }
        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var cacheKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timestamp, count) = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _ratelimitWindow;
                return (timestamp: dateNow, count: 0);
            });

            if (dateNow - timestamp < _ratelimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }

                _memoryCache.Set(cacheKey, (timestamp, count + 1), _ratelimitWindow);
            }
            else
            {
                _memoryCache.Set(cacheKey, (dateNow, 1), _ratelimitWindow);
            }

            return true;
        }

        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
