using OneWayChat.Domain.Entities;
using OneWayChat.Domain.Services;

namespace OneWayChat.Application.Services;
public class ChatService : IMessageService
{
    public void ProcessMessage(Message message)
    {
        Console.WriteLine($"Message processing: {message.Content} (Sent: {message.Timestamp})");
    }
}
