using RabbitMQ.Client.Events;

namespace MorganMWJ.StockManager.Services;

public interface IMessageHandlerFactoryService
{
    void Receive(object? sender, BasicDeliverEventArgs ea);
}