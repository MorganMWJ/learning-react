namespace CheckoutApi.V1.Models;

public class StockItem
{
    public required string Id { get; set; }

    public decimal Price { get; set; }

    public BulkOffer? BulkOffer { get; set; }

    public decimal CalculatePrice(uint quantity)
    {
        decimal total = 0;

        if (BulkOffer is null)
        {
            total += Price * quantity;
        }
        else
        {
            if (quantity < BulkOffer.Quantity)
            {
                total += Price * quantity;
            }
            else
            {
                total += (quantity / BulkOffer.Quantity) * BulkOffer.Price;
                total += (quantity % BulkOffer.Quantity) * Price;
            }
        }

        return total;
    } 
}
