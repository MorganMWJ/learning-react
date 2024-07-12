using CheckoutApi.Services;
using CheckoutApi.V1.Models;
using FluentAssertions;

namespace CheckoutApi.Tests.Unit
{
    public class StockItemTests
    {
        private readonly IStockLookupService _stockLookupService = new MockStockLookupService();

        [TestCase("A", 50, 2180)]
        [TestCase("A", 3, 130)]
        [TestCase("A", 1, 50)]
        [TestCase("B", 20, 450)]
        [TestCase("B", 21, 480)]
        [TestCase("B", 1, 30)]
        [TestCase("C", 1, 20)]
        [TestCase("C", 34, 680)]
        [TestCase("D", 1, 15)]
        [TestCase("D", 34, 510)]
        [TestCase("A", 0, 0)]
        [TestCase("B", 0, 0)]
        [TestCase("C", 0, 0)]
        [TestCase("D", 0, 0)]
        public void CalculatePriceTests(string stockItem, int quantity, decimal expectedPrice)
        {
            var sut = _stockLookupService.GetStockItemByName(stockItem);
            sut.CalculatePrice((uint)quantity).Should().Be(expectedPrice);
        }
    }
}