namespace CheckoutApi.V1.Models;

public class BulkOffer
{
    public BulkOffer(uint quantity, decimal price)
    {
        Quantity = quantity;
        Price = price;
    }

    public uint Quantity { get; set; }

    public decimal Price { get; set; }
}
