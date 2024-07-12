using CheckoutApi.Services;
using CheckoutApi.Services.Implementations;
using CheckoutApi.V1.Models.Requests;
using FluentAssertions;

namespace CheckoutApi.Tests.Unit;

public class CheckoutServiceTests
{
    private readonly IStockLookupService _mockstockLookupService = new MockStockLookupService();
    private readonly ICheckoutService _sut = new CheckoutService(new MockStockLookupService());

    [TestCase("", 0)]
    [TestCase("A", 50)]
    [TestCase("AB", 80)]
    [TestCase("CDBA", 115)]
    [TestCase("AA", 100)]
    [TestCase("AAA", 130)]
    [TestCase("AAABB", 175)]
    [TestCase("AABBCCDD", 215)]
    public void CalculateCheckoutTotalTests(string encodedBasket, decimal expectedTotalPrice)
    {
        var input = ToCollection(encodedBasket);

        var result = _sut.CalculateCheckoutTotal(input);

        result.Should().Be(expectedTotalPrice);
    }

    private IEnumerable<CheckoutItem> ToCollection(string encodedBasket)
    {
        var result = new List<CheckoutItem>();

        foreach(var si in _mockstockLookupService.GetAllStockItems())
        {
            if (encodedBasket.Contains(si.Id))
            {
                result.Add(new CheckoutItem
                {
                    Id = si.Id,
                    Quantity = (uint)encodedBasket.Count(x => x == si.Id[0])
                });
            }            
        }

        return result;
    }
}
