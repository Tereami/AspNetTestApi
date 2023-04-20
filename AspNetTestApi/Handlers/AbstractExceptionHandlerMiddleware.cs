using System.Net;

namespace AspNetTestApi.Handlers;

public abstract class AbstractExceptionHandlerMiddleware
{
    public static string LocalizationKey => "LocalizationKey";

    private readonly RequestDelegate _next;

    public abstract (HttpStatusCode code, string message) GetResponse(PlatformException exception);

    public AbstractExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (PlatformException exception)
        {
            // Логирование ошибки (не знаю, какой логгер) 
            // Logger.Error(exception, "error during executing {Context}", context.Request.Path.Value);
            var response = context.Response;
            response.ContentType = "application/json";

            // get the response code and message
            var (status, message) = GetResponse(exception);
            response.StatusCode = (int)status;
            await response.WriteAsync(message);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = 500;
            await response.WriteAsync($"Unhandled error: {ex.Message}");
        }
    }
}