using System.Collections.Generic;
using System.IO;
using ApplicationName.Library.Impl.Configuration;
using ApplicationName.Library.Impl.Mappers.Extension;
using ApplicationName.Repository.Impl.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;

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
            
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddMicrosoftIdentityWebApi(Configuration, "AzureAd");
                  
            services.AddCustomSwagger();
            services.AddCustomHealthChecks();
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
            app.UseCustomSwaggerUi( pathBase, descriptionProvider);

            app.UseRouting();
            // app.UseAuthentication();
            // app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", 
                    new HealthCheckOptions
                {
                    ResponseWriter = ApplicationBuilderWriteResponseExtension.WriteResponse
                });
                endpoints.MapControllers();
            });
        }
    }
    
}

#pragma warning restore