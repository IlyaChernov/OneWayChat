using OneWayChat.Domain.Entities;

namespace OneWayChat.Domain.Services;

public interface IMessageService
{
    void ProcessMessage(Message message);
}
