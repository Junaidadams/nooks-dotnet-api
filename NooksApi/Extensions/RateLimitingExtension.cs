using Microsoft.AspNetCore.RateLimiting;

namespace NooksApi.Extensions;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddApiRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("Admin", config =>
            {
                config.PermitLimit = 100;
                config.Window = TimeSpan.FromMinutes(1);
                config.QueueLimit = 0;
            });
        });

        return services;
    }
}
