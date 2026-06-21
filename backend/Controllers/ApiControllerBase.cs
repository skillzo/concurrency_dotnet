using backend.Common;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult Send<T>(ServiceResponse<T> response) =>
        StatusCode(response.StatusCode, response);

    protected IActionResult Success<T>(T data, string message = "OK", int statusCode = StatusCodes.Status200OK) =>
        Send(ServiceResponse<T>.SuccessResponse(message, data, statusCode));

    protected IActionResult PaginatedSuccess<T>(
        IReadOnlyList<T> items,
        int total,
        int page,
        int pageSize,
        string message = "OK",
        int statusCode = StatusCodes.Status200OK) =>
        Success(PagedResult<T>.Create(items, total, page, pageSize), message, statusCode);

    protected IActionResult Failure(
        string message,
        int statusCode = StatusCodes.Status400BadRequest,
        ServiceError? error = null) =>
        Send(ServiceResponse<object>.Failure(message, error, statusCode));

    protected IActionResult UnauthorizedResponse(string message) =>
        Failure(message, StatusCodes.Status401Unauthorized);

    protected IActionResult ValidationErrorResponse()
    {
        var errors = ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray());

        return Send(ServiceResponse<Dictionary<string, string[]>>.ValidationFailure(errors));
    }
}
