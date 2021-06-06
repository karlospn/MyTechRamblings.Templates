using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ApplicationName.RabbitConsumer.Worker.Bootstrap.Rabbit
{
    public class RabbitConfigurationProvider 
    {
        private readonly IConfiguration _configuration;

        public RabbitConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RabbitMqConnection GetRabbitMqConnection()
        {
            var rabbitConfig = new RabbitMqConnection();
            _configuration.GetSection("RabbitMqConnection")
                .Bind(rabbitConfig);

            var isEmpty = rabbitConfig.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => (string)pi.GetValue(rabbitConfig))
                .Any(string.IsNullOrEmpty);

            if (isEmpty)
                throw new Exception("Missing some configuration attributes");

            return rabbitConfig;
        }
    }
}
