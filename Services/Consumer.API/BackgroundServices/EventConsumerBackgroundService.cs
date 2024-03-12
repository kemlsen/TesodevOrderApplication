using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Consumer.API.Entities;
using Consumer.API.Context;

namespace Consumer.API.BackgroundServices
{
    public class EventConsumerBackgroundService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly ConsumerDbContext _consumerDbContext;
        private readonly IModel _channel;
        private readonly string QueueName = "queue-order";
        public static string ExchangeName = "order-direct-exchange";
        public static string RoutingKey = "order";
        public EventConsumerBackgroundService(ConsumerDbContext consumerDbContext)
        {
            var factory = new ConnectionFactory() { Uri = new Uri("amqps://tqkqwmct:o8ZY8WkuJ3cFlTmfUlCqIS2ghVCU1Njn@shark.rmq.cloudamqp.com/tqkqwmct") };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: ExchangeType.Direct, true, false);
            _channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);
            _consumerDbContext = consumerDbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                _channel.Dispose();
                _connection.Dispose();
            });
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                using (var dbContext = new ConsumerDbContext())
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var data = JsonSerializer.Deserialize<Audit>(message);
                    await dbContext.AddAsync(data);
                    await dbContext.SaveChangesAsync();
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
