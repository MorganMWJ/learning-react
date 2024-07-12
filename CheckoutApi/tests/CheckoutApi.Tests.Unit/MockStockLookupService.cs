using CheckoutApi.Services;
using CheckoutApi.V1.Models;

namespace CheckoutApi.Tests.Unit;

internal class MockStockLookupService : IStockLookupService
{
    public IEnumerable<StockItem> GetAllStockItems()
    {
        return StockDict.Values;
    }

    public StockItem GetStockItemByName(string name)
    {
        return StockDict[name];
    }

    public bool IsValidStockItem(string name)
    {
        throw new NotImplementedException();
    }

    private static Dictionary<string, StockItem> StockDict => 
        new Dictionary<string, StockItem>()
        {
            { "A", new StockItem{ Id = "A", Price = 50.00m, BulkOffer = new BulkOffer(3, 130.00m)} },
            { "B", new StockItem{ Id = "B", Price = 30.00m, BulkOffer = new BulkOffer(2, 45.00m)} },
            { "C", new StockItem{ Id = "C", Price = 20.00m, BulkOffer = null} },
            { "D", new StockItem{ Id = "D", Price = 15.00m, BulkOffer = null} },
        };
}
