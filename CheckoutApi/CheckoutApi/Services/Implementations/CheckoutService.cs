using CheckoutApi.V1.Models.Requests;

namespace CheckoutApi.Services.Implementations;

public class CheckoutService : ICheckoutService
{
    private readonly IStockLookupService _stockLookupService;

    public CheckoutService(IStockLookupService stockLookupService)
    {
        _stockLookupService = stockLookupService;
    }

    public decimal CalculateCheckoutTotal(IEnumerable<CheckoutItem> items)
    {
        decimal total = 0;

        foreach (var item in items)
        {
            var stockItem = _stockLookupService.GetStockItemByName(item.Id);

            total += stockItem.CalculatePrice(item.Quantity);
        }

        return total;
    }
}
