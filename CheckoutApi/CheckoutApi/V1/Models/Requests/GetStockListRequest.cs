using MediatR;

namespace CheckoutApi.V1.Models.Requests;

public class GetStockListRequest : IRequest<IEnumerable<StockItem>>
{ 
}
