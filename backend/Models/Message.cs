namespace backend.Models;

public class Message
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }

    public required string Content { get; set; }
    public DateTime SentAt { get; set; }

    public Room Room { get; set; } = null!;
    public User Sender { get; set; } = null!;
}