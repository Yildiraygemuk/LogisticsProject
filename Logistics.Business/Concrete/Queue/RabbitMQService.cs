using Logistics.Core.Utilities.Results;
using Logistics.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Logistics.Business
{
    public class RabbitMQService: IRabbitMQService
    {
        private readonly IConfiguration _configuration;

        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IResult SendToQueue(Order order)
        {
            var rabbitMQUri = _configuration.GetSection("RabbitMQUri").Value;
            var factory = new ConnectionFactory() { Uri = new Uri(rabbitMQUri) };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "LogisticsOrder",
                                 durable: false,
                                 exclusive: false,
            autoDelete: false,
                                 arguments: null);

            var messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order));
            channel.BasicPublish(exchange: "", routingKey: "LogisticsOrder", basicProperties: null, body: messageBytes);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: {0}", message);
            };
            return new SuccessResult();
        }
    }
}
