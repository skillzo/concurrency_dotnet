using backend.DTOs;
using backend.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationErrorResponse();

        var result = await _auth.RegisterAsync(request);
        return Success(result, "Registration successful.", StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationErrorResponse();

        var result = await _auth.LoginAsync(request);
        if (result is null)
            return UnauthorizedResponse("Invalid credentials.");

        return Success(result);
    }
}
