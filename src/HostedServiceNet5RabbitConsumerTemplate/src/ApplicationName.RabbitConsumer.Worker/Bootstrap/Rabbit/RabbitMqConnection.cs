namespace ApplicationName.RabbitConsumer.Worker.Bootstrap.Rabbit
{
    public class RabbitMqConnection
    {
        public string HostName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string QueueName { get; set; }
    }
}
