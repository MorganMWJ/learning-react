using CheckoutApi.Services;
using CheckoutApi.V1.Models.Requests;
using CheckoutApi.V1.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.Features;

namespace CheckoutApi.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckoutRequest, CheckoutResponse>
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IMessageSenderService _messageSenderService;

        public CheckoutHandler(ICheckoutService checkoutService, 
            IMessageSenderService messageSenderService)
        {
            _checkoutService = checkoutService;
            _messageSenderService = messageSenderService;
        }

        public Task<CheckoutResponse> Handle(CheckoutRequest request, CancellationToken cancellationToken)
        {
            var result = _checkoutService.CalculateCheckoutTotal(request.Items);

            _messageSenderService.SendMessageAsync(new { request.Items, TotalPrice = result});

            // Could do other checkout operations here...

            return Task.FromResult(new CheckoutResponse
            {
                TotalPrice = result
            });
        }
    }
}
