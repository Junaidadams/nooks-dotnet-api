namespace NooksApi.Extensions;

public static class ForwardedHeaderExtensions
{
    public static IServiceCollection AddCloudflareForwardedHeadersConfiguration(this IServiceCollection services)
    {
        return services;
    }
}
