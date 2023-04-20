using System.Net;
using Microsoft.Extensions.Localization;

namespace AspNetTestApi.Handlers;

public class PlatformExceptionHandlerMiddleware : AbstractExceptionHandlerMiddleware
{
    private readonly IStringLocalizer<PlatformExceptionHandlerMiddleware> _localizer;
    public PlatformExceptionHandlerMiddleware(RequestDelegate next) : base(next)
    {
    }

    public override (HttpStatusCode code, string message) GetResponse(PlatformException exception)
    {
        var code = exception.ErrorType switch
        {
            ErrorTypeEnum.Unauthorized => HttpStatusCode.Unauthorized,
            ErrorTypeEnum.Forbidden => HttpStatusCode.Forbidden,
            ErrorTypeEnum.EntityNotFound or ErrorTypeEnum.FileNotFound => HttpStatusCode.NotFound,
            _ => HttpStatusCode.NotImplemented
        };

        return (code, exception.Message);
    }
}