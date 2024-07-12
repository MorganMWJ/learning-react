using CheckoutApi.V1.Models;

namespace CheckoutApi.Services;

public interface IStockLookupService
{
    bool IsValidStockItem(string name);

    StockItem GetStockItemByName(string name);

    IEnumerable<StockItem> GetAllStockItems();
}
