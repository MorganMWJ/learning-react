using CheckoutApi.V1.Models.Responses;
using System.Net.Http.Json;
using System.Net;
using CheckoutApi.V1.Models.Requests;
using CheckoutApi.Authentication;

namespace CheckoutApi.Tests.Component;

[TestFixture]
public class ApiKeyAuthFilterTests
{
    private const string Endpoint = "v1/checkout";

    private readonly TestApplicationFactory _factory;
    private readonly HttpClient _client;

    public ApiKeyAuthFilterTests()
    {
        _factory = new TestApplicationFactory();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task Checkout_returns_unauth_when_missing_api_key_header()
    {
        // Arrange
        var httpJsonContent = JsonContent.Create(ValidCheckoutRequest);
        _ = _client.DefaultRequestHeaders.Remove(ApiKeyAuthFilter.ApiKeyHeader);

        // Act
        var response = await _client.PostAsync(Endpoint, httpJsonContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Contain($"API Key missing");
    }

    [Test]
    public async Task Checkout_returns_unauth_when_invalid_api_key()
    {
        // Arrange
        var httpJsonContent = JsonContent.Create(ValidCheckoutRequest);
        _client.DefaultRequestHeaders.Add(ApiKeyAuthFilter.ApiKeyHeader, "incorrectApiKey123");

        // Act
        var response = await _client.PostAsync(Endpoint, httpJsonContent);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Contain($"Invalid API Key");
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
}
