using System.Threading.Tasks;
using ApplicationName.Library.Contracts;
using ApplicationName.Library.Contracts.Dto;
using ApplicationName.Library.Impl.Configuration;
using ApplicationName.Repository.Impl.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;
using Xunit.Sdk;

namespace ApplicationName.RabbitConsumer.Worker.IntegrationTest
{
    public class FooServiceTests
    {

        [Fact]
        public async Task IfAFooDtoIsReceived_ThenIsStoredInTheDatabaseCorrectly()
        {
            var provider = InitializeDependencies();
            var service = provider.GetService<IFooService>();
            if (service == null)
                throw new NullException("FooService is null");
            await service.DoSomeProcessingAsync(CreateFooDto());
        }

        private ServiceProvider InitializeDependencies()
        {
            var services = new ServiceCollection();
            services.AddLibraryServices();
            services.AddRepositoryServices();
            services.AddScoped(typeof(ILogger<>), typeof(Logger<>));
            services.AddSingleton(new LoggerFactory().AddSerilog());
            return services.BuildServiceProvider();
        }

        private FooDto CreateFooDto()
        {
            return new()
            {
                Data = "abc"
            };

        }
    }
}
