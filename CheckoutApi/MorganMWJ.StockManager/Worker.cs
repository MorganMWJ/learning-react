using MorganMWJ.StockManager.Services;

namespace MorganMWJ.StockManager
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueListenerService _queueListener;

        public Worker(ILogger<Worker> logger, IQueueListenerService queueListener)
        {
            _logger = logger;
            _queueListener = queueListener;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                _queueListener.ListenForMessages();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
