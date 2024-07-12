using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MorganMWJ.StockManager.Services.Implementation;

public class RabbitQueueListenerService<T> : IQueueListenerService
{
    private readonly ConnectionFactory _factory;
    private readonly IMessageHandlerFactoryService _handlerFactory;
    private readonly RabbitQueueListenerServiceOptions _options;

    public RabbitQueueListenerService(IMessageHandlerFactoryService handler,
        IOptions<RabbitQueueListenerServiceOptions> options)
    {
        _options = options.Value;
        _factory = new ConnectionFactory { HostName = _options.Hostname};
        _handlerFactory = handler.CreateMessageHandler(); ;        
    }

    public void ListenForMessages()
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _options.QueueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");

            channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
        };        

        channel.BasicConsume(queue: _options.QueueName,
                             autoAck: false,
                             consumer: consumer);
    }
}
