namespace CheckoutApi.Services;

public interface IMessageSenderService
{
    void SendMessageAsync(object message);
}
