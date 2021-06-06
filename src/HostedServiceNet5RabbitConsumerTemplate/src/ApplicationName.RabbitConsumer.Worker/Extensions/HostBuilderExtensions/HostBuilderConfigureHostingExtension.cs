namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderConfigureHostingExtension
    {
        public static IHostBuilder ConfigureHosting(this IHostBuilder builder)
        {
            builder.UseWindowsService();
            builder.UseSystemd();
            return builder;
        }
    }
}
