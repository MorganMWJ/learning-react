using CheckoutApi.V1.Models;

namespace CheckoutApi.Services.Implementations;

public class StockLookupServiceOptions
{
    public const string StockOptions = nameof(StockOptions);

    public required IDictionary<string, StockItem> Stock {  get; set; }
}