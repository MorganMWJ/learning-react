using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CheckoutApi.Services.Implementations;

public class MessageSenderService : IMessageSenderService
{
    private readonly ConnectionFactory _factory;

    public MessageSenderService()
    {
        _factory = new ConnectionFactory { HostName = "localhost" }; //use DI options
    }

    public void SendMessageAsync(object message)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Declaring a queue is idempotent
        channel.QueueDeclare(queue: "checkoutapi-checkout-queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: "checkoutapi-checkout-queue",
                             basicProperties: null,
                             body: body);
    }
}
