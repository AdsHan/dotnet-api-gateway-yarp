using Microsoft.AspNetCore.RateLimiting;
using System.Globalization;
using System.Threading.RateLimiting;

namespace Project.Gateway.Configuration;

public static class RateLimitConfig
{
    public static IServiceCollection AddRateLimitConfiguration(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimitoptions =>
        {
            rateLimitoptions.OnRejected = (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter =
                        ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                }

                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.WriteAsync("Too many requests.");

                return new ValueTask();
            };

            rateLimitoptions.AddFixedWindowLimiter("fixedLimiterPolicy", options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.PermitLimit = 4;
            });
        });

        return services;
    }

    public static WebApplication UseRateLimitConfiguration(this WebApplication app)
    {
        app.UseRateLimiter();

        return app;
    }
}



