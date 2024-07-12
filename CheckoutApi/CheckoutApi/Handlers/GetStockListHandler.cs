using CheckoutApi.Services;
using CheckoutApi.V1.Models;
using CheckoutApi.V1.Models.Requests;
using MediatR;

namespace CheckoutApi.Handlers
{
    public class GetStockListHandler : IRequestHandler<GetStockListRequest, IEnumerable<StockItem>>
    {
        private readonly IStockLookupService _stockLookupService;

        public GetStockListHandler(IStockLookupService stockLookupService)
        {
            _stockLookupService = stockLookupService;
        }

        public Task<IEnumerable<StockItem>> Handle(GetStockListRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_stockLookupService.GetAllStockItems());
        }
    }
}
