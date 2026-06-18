
namespace backend.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Username { get; set; }

    public required string Email { get; set; }
    public string PasswordHash { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = new List<Message>();




}