namespace backend.Common;

public record ServiceResponse<T>(
    bool Success,
    string Message,
    T? Data,
    int StatusCode,
    ServiceError? Error = null)
{
    public static ServiceResponse<T> SuccessResponse(
        string message,
        T? data = default,
        int statusCode = StatusCodes.Status200OK) =>
        new(true, message, data, statusCode);

    public static ServiceResponse<T> Failure(
        string message,
        ServiceError? error = null,
        int statusCode = StatusCodes.Status400BadRequest) =>
        new(false, message, default, statusCode, error);

    public static ServiceResponse<T> Failure(
        string message,
        Exception exception,
        int statusCode = StatusCodes.Status400BadRequest) =>
        new(false, message, default, statusCode, new ServiceError(exception.GetType().Name, exception.Message));

    public static ServiceResponse<Dictionary<string, string[]>> ValidationFailure(
        Dictionary<string, string[]> errors) =>
        new(false, "Validation failed.", errors, StatusCodes.Status400BadRequest);
}
