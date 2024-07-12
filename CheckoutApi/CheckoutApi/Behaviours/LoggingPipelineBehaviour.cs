using CheckoutApi.V1.Models.Requests;
using CheckoutApi.V1.Models.Responses;
using MediatR;
using System.Text.Json;

namespace CheckoutApi.Behaviours
{
    public class LoggingPipelineBehaviour : IPipelineBehavior<CheckoutRequest, CheckoutResponse>
    {
        private readonly ILogger<LoggingPipelineBehaviour> _logger;

        public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour> logger)
        {
            _logger = logger;
        }

        public async Task<CheckoutResponse> Handle(CheckoutRequest request, RequestHandlerDelegate<CheckoutResponse> next, CancellationToken cancellationToken)
        {
            // Pre-processing          
            _logger.LogInformation($"Handling {typeof(CheckoutResponse).Name}");
            _logger.LogInformation(JsonSerializer.Serialize(request));

            var response = await next();

            // Post-processing
            _logger.LogInformation(JsonSerializer.Serialize(response));

            return response;
        }
    }
}
