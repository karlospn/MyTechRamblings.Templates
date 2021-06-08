using System.Collections.Generic;
using System.IO;
using ApplicationName.Library.Impl.Configuration;
using ApplicationName.Library.Impl.Mappers.Extension;
using ApplicationName.Repository.Impl.Configuration;
using Microsoft.AspNetCore.Builder;
#if HealthCheck
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#endif
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;
#if Authorization
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
#endif

// Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 


namespace ApplicationName.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomApiVersioning();
            services.AddAutoMapper(typeof(AutoMapperLibraryExtension).Assembly);
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters = new List<Newtonsoft.Json.JsonConverter>()
                    {
                        new StringEnumConverter()
                    };
                });

#if Authorization            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration, "AzureAd");
#endif

#if Swagger             
            services.AddCustomSwagger();
#endif

#if HealthCheck
            services.AddCustomHealthChecks();
#endif
            services.AddLibraryServices()
                    .AddRepositoryServices();

        }
        
        public virtual void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IApiVersionDescriptionProvider descriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var pathBase = Configuration.GetValue<string>("PATH_BASE");
            app.UsePathBase(pathBase);
#if Swagger
            app.UseCustomSwaggerUi( pathBase, descriptionProvider);
#endif
            app.UseRouting();
#if Authorization
            app.UseAuthentication();
            app.UseAuthorization();
#endif
            app.UseEndpoints(endpoints =>
            {
#if HealthCheck
                endpoints.MapHealthChecks("HEALTHCHECK-PATH", 
                    new HealthCheckOptions
                {
                    ResponseWriter = ApplicationBuilderWriteResponseExtension.WriteResponse
                });
#endif
                endpoints.MapControllers();
            });
        }
    }
    
}

#pragma warning restore