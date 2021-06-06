using Microsoft.Extensions.Hosting;

namespace ApplicationName.RabbitConsumer.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHosting()
                .ConfigureLogging()
                .RegisterDependencies();
    }
}


