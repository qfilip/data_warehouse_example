using System.Net;
using System.Text.Json;

namespace DwHouse.Api.Middlewares;

public class AppHttpMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AppHttpMiddleware> _logger;

    public AppHttpMiddleware(RequestDelegate next, ILogger<AppHttpMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var errorDetails = new HttpRequestErrorDetails();

        errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
        errorDetails.Message = ex.Message;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorDetails.StatusCode;

        await context.Response.WriteAsync(errorDetails.ToString());
    }
}

internal class HttpRequestErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}