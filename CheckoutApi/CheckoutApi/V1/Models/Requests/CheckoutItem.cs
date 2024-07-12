using CheckoutApi.V1.Validation;
using System.ComponentModel.DataAnnotations;

namespace CheckoutApi.V1.Models.Requests;

public class CheckoutItem
{
    [Required]
    [ValidCheckoutItem]
    public required string Id { get; set; }

    [Required]
    public uint Quantity { get; set; }
}
