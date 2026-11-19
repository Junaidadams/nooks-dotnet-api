using NooksApi.Exceptions;

namespace NooksApi.Extensions;

public static class ConfigurationExtensions
{
    public static T TryGetSection<T>(this Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        var sectionName = typeof(T).Name;

        var result = configuration
            .GetSection(sectionName)
            .Get<T>();

        var name = typeof(T).Name;

        if (result is null)
        {
            throw new MissingConfigurationException($"{name} are missing from the configuration.");
        }

        return result;
    }
}
