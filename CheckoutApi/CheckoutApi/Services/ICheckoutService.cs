using CheckoutApi.V1.Models.Requests;

namespace CheckoutApi.Services;

public interface ICheckoutService
{
    decimal CalculateCheckoutTotal(IEnumerable<CheckoutItem> items);
}
