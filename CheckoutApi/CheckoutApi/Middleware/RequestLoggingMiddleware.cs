using System.Diagnostics;
using System.Text;

namespace CheckoutApi.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        if (context.Request.HasJsonContentType())
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                context.Request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var postData = Encoding.UTF8.GetString(buffer);
                context.Request.Body.Position = 0;

                _logger.LogInformation("Request-Json-Content: {postData}", postData);
            }            
        }

        await _next(context);

        watch.Stop();
        _logger.LogInformation($"Elapsed milliseconds: {watch.ElapsedMilliseconds}");
    }
}
