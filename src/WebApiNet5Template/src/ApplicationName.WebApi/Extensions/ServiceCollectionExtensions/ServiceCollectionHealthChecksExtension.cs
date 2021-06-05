#pragma warning disable CS1591 

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionHealthChecksExtension
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }
    }
}

#pragma warning restore