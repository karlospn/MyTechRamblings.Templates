using ApplicationName.Repository.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationName.Repository.Impl.Configuration
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IFooRepository, FooRepository>();    
            return services;
        }
    }
}
