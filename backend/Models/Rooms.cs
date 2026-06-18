namespace backend.Models;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}