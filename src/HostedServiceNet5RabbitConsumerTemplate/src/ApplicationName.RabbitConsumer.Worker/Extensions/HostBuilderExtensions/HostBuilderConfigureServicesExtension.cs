using ApplicationName.Library.Impl.Configuration;
using ApplicationName.RabbitConsumer.Worker;
using ApplicationName.Repository.Impl.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderConfigureServicesExtension
    {
        public static IHostBuilder RegisterDependencies(this IHostBuilder builder )
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddLibraryServices();
                services.AddRepositoryServices();
            });
            
            return builder;
        }

        public static IHostBuilder ValidateDependencies(this IHostBuilder builder)
        {
            builder.UseDefaultServiceProvider((context, options) =>
            {
                var isDevelopment = context.HostingEnvironment.IsDevelopment();
                options.ValidateScopes = isDevelopment;
                options.ValidateOnBuild = isDevelopment;
            });
            return builder;
        }

    }
}