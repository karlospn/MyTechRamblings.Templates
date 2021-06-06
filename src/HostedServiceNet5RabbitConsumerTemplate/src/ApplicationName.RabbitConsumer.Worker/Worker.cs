using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicationName.Library.Contracts;
using ApplicationName.RabbitConsumer.Worker.Bootstrap.Rabbit;
using ApplicationName.RabbitConsumer.Worker.Mapper.Extension;
using ApplicationName.RabbitConsumer.Worker.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace ApplicationName.RabbitConsumer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqConnection _rabbitConfig;
        private readonly IFooService _service;

        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public Worker(ILogger<Worker> logger,
            IConfiguration configuration, 
            IFooService service)
        {
            _logger = logger;
            _service = service;

            _rabbitConfig = new RabbitConfigurationProvider(configuration)
                .GetRabbitMqConnection();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitConfig.HostName,
                Port = _rabbitConfig.Port,
                UserName = _rabbitConfig.User,
                Password = _rabbitConfig.Password,
                DispatchConsumersAsync = true

            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_rabbitConfig.QueueName, durable: true, exclusive: false, autoDelete: false);
            _channel.BasicQos(0, 1, false);
            _logger.LogInformation($"Worker is waiting for messages incoming in the following queue: {_rabbitConfig.QueueName}...");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation($"Received a message: '{message}'.");
                
                try
                {
                    var item = JsonConvert.DeserializeObject<FooMessage>(message);
                    await _service.DoSomeProcessingAsync(item.ToDto());

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (AlreadyClosedException e)
                {
                    _logger.LogError("RabbitMQ is closed", e);
                    _channel.BasicReject(ea.DeliveryTag, true);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Something went wrong went trying to process the message. {e.Message}");
                    _channel.BasicReject(ea.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(queue: _rabbitConfig.QueueName, autoAck: false, consumer: consumer);
            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }

    }
}
