using Serilog;
using Serilog.Events;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderConfigureLoggingExtension
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder builder )
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.UseSerilog();
            return builder;
        }
    }
}
