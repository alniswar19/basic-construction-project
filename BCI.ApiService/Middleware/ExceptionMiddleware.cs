using BCI.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace BCI.ApiService.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (RecordNotFoundException ex)
        {
            _logger.LogError(ex.Message);

            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            StatusCode = context.Response.StatusCode,
            Message = statusCode switch
            {
                HttpStatusCode.InternalServerError => $"Uncaught Exception. {exception.Message}",
                _ => exception.Message
            }
        }));
    }
}