using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

#pragma warning disable CS1591 

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionApiVersioningExtension
    {
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services
                .AddApiVersioning(opt =>
                {
                    opt.ReportApiVersions = true;
                    opt.AssumeDefaultVersionWhenUnspecified = true;
                    opt.DefaultApiVersion = new ApiVersion(1, 0);
                    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}

#pragma warning restore