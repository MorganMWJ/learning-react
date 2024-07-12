using RabbitMQ.Client.Events;
using System.Text;

namespace MorganMWJ.StockManager.Services.Implementation;

public class MessageHandlerFactoryService : IMessageHandlerFactoryService
{
    public IMessageHandler CreateMessageHandler(Type messageType)
    {
        return messageType switch
        {
            typeof(CheckoutMessage) => new CheckoutMessageHandler(),
            _ => throw new ArgumentOutOfRangeException("No handler for message type")
        };
    }
}
