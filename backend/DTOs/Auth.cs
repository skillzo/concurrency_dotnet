using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class RegisterRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = null!;
}

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public record AuthResponse(string Token, Guid UserId, string Username);
