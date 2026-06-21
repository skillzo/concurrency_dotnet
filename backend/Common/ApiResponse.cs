namespace backend.Common;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    string? Message = null,
    Dictionary<string, string[]>? Errors = null)
{
    public static ApiResponse<T> Ok(T data) => new(true, data);

    public static ApiResponse<T> Fail(string message) => new(false, default, message);

    public static ApiResponse<T> ValidationFail(Dictionary<string, string[]> errors)
        => new(false, default, "Validation failed.", errors);
}
