using CheckoutApi.Authentication;
using CheckoutApi.Services;
using CheckoutApi.V1.Models.Requests;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutApi.Tests.Component;

internal class TestApplicationFactory : WebApplicationFactory<Program>
{
    internal Mock<ICheckoutService> MockCheckoutService { get; set; }

    protected override void ConfigureClient(HttpClient client)
    {
        client.DefaultRequestHeaders.Add(ApiKeyAuthFilter.ApiKeyHeader, "0543ef62c7df4b8c97fab1cab476491c");
    }

    internal HttpClient CreateClientWithMockCheckoutServiceError()
    {
        MockCheckoutService = new Mock<ICheckoutService>();
        MockCheckoutService.Setup(x => x.CalculateCheckoutTotal(It.IsAny<IEnumerable<CheckoutItem>>()))
            .Throws(new Exception("Test Exception"));

        var client = this.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<ICheckoutService>(_ => MockCheckoutService.Object);
            });
        })
         .CreateClient();

        return client;
    }
}
