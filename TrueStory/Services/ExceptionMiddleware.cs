using TrueStory.Models;

namespace TrueStory.Services;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
            _logger.LogDebug("Handled: {Url}", context.Request.Path);
        }
        catch (HttpRequestException requestException)
        {
            _logger.LogError(requestException, "Error while executing request {Url}", context.Request.Path);
            var responseModel = new ExceptionResponseModel()
            {
                OriginalMessage = requestException.InnerException?.Message,
                OriginalStatusCode = requestException.StatusCode.ToString(),
                OriginalUrl = requestException.TargetSite?.Name
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(responseModel);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while executing request {Url}", context.Request.Path);
            var responseModel = new ExceptionResponseModel()
            {
                OriginalMessage = "Something went wrong",
                OriginalStatusCode = StatusCodes.Status500InternalServerError.ToString(),
                OriginalUrl = "/"
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(responseModel);
        }
    }
}