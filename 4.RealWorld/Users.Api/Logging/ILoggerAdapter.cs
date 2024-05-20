namespace Users.Api.Logging;

public interface ILoggerAdapter<TType> where TType : class
{
    void LogInformation(string? message, params object?[] args);
    void LogError(Exception? exception, string? message, params object?[] args);
}
