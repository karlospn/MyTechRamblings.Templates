using System.Reflection;
using Microsoft.Extensions.Hosting;

#pragma warning disable CS1591 

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationBuilderExtension
    {
        public static IConfigurationBuilder AddJsonFiles(this IConfigurationBuilder builder,
            HostBuilderContext builderContext)
        {
            IHostEnvironment env = builderContext.HostingEnvironment;

            bool reloadOnChange = builderContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", defaultValue: true);

            builder.AddJsonFile("settings.json", optional: true, reloadOnChange: reloadOnChange)
                .AddJsonFile($"settings.{env.EnvironmentName}.json", optional: true, reloadOnChange: reloadOnChange);

            if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
            {
                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                builder.AddUserSecrets(appAssembly, optional: true);
            }

            builder.AddEnvironmentVariables();
            return builder;
        }
    }
}

#pragma warning restore