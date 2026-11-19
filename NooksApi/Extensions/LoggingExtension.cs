using Serilog;

namespace NooksApi.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddApiLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        builder.Host.UseSerilog();
        return builder;
    }
}
