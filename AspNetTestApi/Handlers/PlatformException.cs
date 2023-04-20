namespace AspNetTestApi.Handlers;

public class PlatformException : Exception
{
    public ErrorTypeEnum ErrorType { get; init; }
    public PlatformException(string baseMessage, ErrorTypeEnum errorType) : base($"Platform Exception: {baseMessage}")
    {
        ErrorType = errorType;
    }
}