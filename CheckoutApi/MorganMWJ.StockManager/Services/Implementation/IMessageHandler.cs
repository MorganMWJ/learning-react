namespace MorganMWJ.StockManager.Services.Implementation;

public interface IMessageHandler<T>
{
    void HandleMessage(T message);
}