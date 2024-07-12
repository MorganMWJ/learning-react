using CheckoutApi.Authentication;
using CheckoutApi.V1.Models.Requests;
using CheckoutApi.V1.Models.Responses;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CheckoutApi.Tests.Component
{
    [TestFixture]
    public class CheckoutControllerTests
    {
        private const string Endpoint = "v1/checkout";

        private readonly TestApplicationFactory _factory;

        private HttpClient _client;

        public CheckoutControllerTests()
        {
            _factory = new TestApplicationFactory();
        }

        [SetUp]
        public void SetUp()
        {
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Checkout_returns_accepted_with_total_price_for_valid_request()
        {
            // Arrange
            var httpJsonContent = JsonContent.Create(ValidCheckoutRequest);

            // Act
            var response = await _client.PostAsync(Endpoint, httpJsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);

            var responseObject = await response.Content.ReadFromJsonAsync<CheckoutResponse>();
            responseObject.Should().NotBeNull();
            responseObject!.TotalPrice.Should().Be(255m);
        }

        [Test]
        public async Task Checkout_returns_accepted_with_total_price_for_valid_request_with_duplicate_checkout_item_entry()
        {
            // Arrange
            var httpJsonContent = JsonContent.Create(AppendCheckoutRequest(new CheckoutItem { Id = "A", Quantity = 1 }));

            // Act
            var response = await _client.PostAsync(Endpoint, httpJsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);

            var responseObject = await response.Content.ReadFromJsonAsync<CheckoutResponse>();
            responseObject.Should().NotBeNull();
            responseObject!.TotalPrice.Should().Be(305m);
        }

        [Test]
        public async Task Checkout_returns_bad_request_for_invalid_checkout_item_ids()
        {
            // Arrange
            var invalidStockId = "RubbishID";
            var invalidCheckoutRequest = AppendCheckoutRequest(new CheckoutItem { Id = invalidStockId, Quantity = 1 });
            var httpJsonContent = JsonContent.Create(invalidCheckoutRequest);

            // Act
            var response = await _client.PostAsync(Endpoint, httpJsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain($"Id with value {invalidStockId} is invalid");
        }

        [Test]
        public async Task Checkout_returns_bad_request_for_negative_checkout_item_quantities()
        {
            // Arrange
            var validJsonString = JsonSerializer.Serialize(ValidCheckoutRequest);
            var invalidJsonString = validJsonString.Replace("4", "-4"); //swap out for negative - cannot set in object as int is unsigned
            var httpStringContent = new StringContent(invalidJsonString, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(Endpoint, httpStringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Checkout_returns_bad_request_for_missing_checkout_items()
        {
            // Arrange
            var invalidCheckoutRequest = ValidCheckoutRequest;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            invalidCheckoutRequest.Items = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            var httpJsonContent = JsonContent.Create(invalidCheckoutRequest);

            // Act
            var response = await _client.PostAsync(Endpoint, httpJsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Checkout_returns_server_error_for_unhandled_exception()
        {
            // Arrange     
            var client = _factory.CreateClientWithMockCheckoutServiceError();

            var httpJsonContent = JsonContent.Create(ValidCheckoutRequest);

            // Act
            var response = await client.PostAsync(Endpoint, httpJsonContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        private CheckoutRequest ValidCheckoutRequest => new CheckoutRequest
        {
            Items = new List<CheckoutItem>
            {
                new CheckoutItem{ Id = "A", Quantity = 4 },
                new CheckoutItem{ Id = "B", Quantity = 2 },
                new CheckoutItem{ Id = "D", Quantity = 2 },
            }
        };

        private CheckoutRequest AppendCheckoutRequest(CheckoutItem badItem)
        {
            return new CheckoutRequest
            {
                Items = ValidCheckoutRequest.Items.Concat(new CheckoutItem[] { badItem })
            };
        }
    }
}
