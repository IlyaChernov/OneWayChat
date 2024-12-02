namespace OneWayChat.Domain.Entities;

public class Message
{
    public string Content { get; private set; }
    public DateTime Timestamp { get; private set; }

    public Message(string content)
    {
        Content = content;
        Timestamp = DateTime.UtcNow; //I dont rememember if it was needed
    }
}
