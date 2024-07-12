using CheckoutApi.V1.Models.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CheckoutApi.V1.Models.Requests;

public class CheckoutRequest : IRequest<CheckoutResponse>
{
    [Required]
    public required IEnumerable<CheckoutItem> Items { get; set; }
}
