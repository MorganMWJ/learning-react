namespace MorganMWJ.StockManager.Services.Implementation;

public class RabbitQueueListenerServiceOptions
{
    public string Hostname { get; set; } = "localhost";

    public string QueueName { get; set; } = "checkoutapi-checkout-queue";
}