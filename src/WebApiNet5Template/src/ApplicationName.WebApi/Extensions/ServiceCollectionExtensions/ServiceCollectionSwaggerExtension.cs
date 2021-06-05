using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

#pragma warning disable CS1591 

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionSwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services
                .AddSwaggerGen(c =>
                {
                    // Uncomment following sections if you use JWT Authorization schema

                    // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    // {
                    //     Description =
                    //         "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    //     Name = "Authorization",
                    //     In = ParameterLocation.Header,
                    //     Type = SecuritySchemeType.ApiKey,
                    //     Scheme = "Bearer"
                    // });
                    //
                    // c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    // {
                    //     {
                    //         new OpenApiSecurityScheme
                    //         {
                    //             Reference = new OpenApiReference
                    //             {
                    //                 Type = ReferenceType.SecurityScheme,
                    //                 Id = "Bearer"
                    //             },
                    //             Scheme = "oauth2",
                    //             Name = "Bearer",
                    //             In = ParameterLocation.Header,
                    //         },
                    //         new List<string>()
                    //     }
                    // });

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerDoc(description.GroupName, 
                            CreateInfoForApiVersion(description));
                    }

                    foreach (var xmlDocumentPath in GetXmlPaths())
                    {
                        c.IncludeXmlComments(xmlDocumentPath);
                    }
                })
                .AddSwaggerGenNewtonsoftSupport();
            
            return services;
        }


        public static IApplicationBuilder UseCustomSwaggerUi(this IApplicationBuilder applicationBuilder, 
                                                             string endpointPrefix, 
                                                             IApiVersionDescriptionProvider provider)
        {
            applicationBuilder.UseSwagger(opts =>
            {
                opts.RouteTemplate = "api-docs/{documentName}/swagger.json";
                opts.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{endpointPrefix}" } };
                });
            });

            applicationBuilder.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"./{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    options.RoutePrefix = "api-docs";
                }
            });

            return applicationBuilder;
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Sample API",
                Version = description.ApiVersion.ToString(),
                Description = "Put your api info",
                TermsOfService = new Uri("https://www.company.com/"),
                Contact = new OpenApiContact
                {
                    Email = "support@company.com",
                    Name = "My Company Support Team"
                },
                License = new OpenApiLicense
                {
                    Name = $"Copyright {DateTime.Now.Year}, My Company Inc. All rights reserved."
                },
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }


        private static IEnumerable<string> GetXmlPaths()
        {
            return Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.AllDirectories)
                .Where(f => f.Contains("WebApi.xml") || f.Contains("Library.Contracts"))
                .ToList();
        }
    }
}

#pragma warning restore