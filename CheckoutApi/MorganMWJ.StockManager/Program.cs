using MorganMWJ.StockManager.Services;
using MorganMWJ.StockManager.Services.Implementation;

namespace MorganMWJ.StockManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddSingleton<IQueueListenerService, RabbitQueueListenerService>();
            builder.Services.AddSingleton<IMessageHandlerFactoryService, RabbitMessageReceiverService>();

            var host = builder.Build();
            host.Run();
        }
    }
}