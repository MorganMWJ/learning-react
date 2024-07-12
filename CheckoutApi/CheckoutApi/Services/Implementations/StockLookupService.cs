using CheckoutApi.V1.Models;
using Microsoft.Extensions.Options;

namespace CheckoutApi.Services.Implementations;

/// <summary>
/// Perhaps these stock items would come from db/ external service
/// for now they are being read from config and injected as options.
/// </summary>
public class StockLookupService : IStockLookupService
{
    private readonly IOptions<StockLookupServiceOptions> _options;

    public StockLookupService(IOptions<StockLookupServiceOptions> options)
    {
        _options = options;
    }

    public bool IsValidStockItem(string name)
    {
        return _options.Value.Stock.ContainsKey(name);
    }

    public StockItem GetStockItemByName(string name)
    {
        return _options.Value.Stock[name];
    }

    public IEnumerable<StockItem> GetAllStockItems()
    {
        return _options.Value.Stock.Values;
    }
}
