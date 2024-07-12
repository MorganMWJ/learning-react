using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CheckoutApi.Authentication;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    public const string ApiKeyHeader = "x-api-key";
    public const string ApiKeyHeaderSectionName = "ApiKey";

    private readonly IConfiguration _configuration;

    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if(!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key missing");
            return;
        }

        var apiKey = _configuration.GetValue<string>(ApiKeyHeaderSectionName)!;
        if (!apiKey.Equals(extractedKey))
        {
            context.Result = new UnauthorizedObjectResult("Invalid API Key");
            return;
        }
    }
}
