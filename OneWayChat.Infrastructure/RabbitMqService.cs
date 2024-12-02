using System.Text;
using OneWayChat.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OneWayChat.Infrastructure;

public class RabbitMqService(string queueName)
{
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly string _queueName = queueName;
    private IChannel Channel => _channel ?? throw new InvalidOperationException("Channel is not initialized. Call InitializeAsync first.");
    private IConnection Connection => _connection ?? throw new InvalidOperationException("Connection is not initialized. Call InitializeAsync first.");

    public async Task InitializeAsync(string hostName)
    {
        var factory = new ConnectionFactory() { HostName = hostName };
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        // Declare the queue (ensure it exists)
        await _channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public async Task SendMessageAsync(Message message)
    {
        var body = Encoding.UTF8.GetBytes(message.Content);
        var properties = new BasicProperties();
        properties.Persistent = true;

        // Publish the message
        await Channel.BasicPublishAsync(
            exchange: "",
            routingKey: _queueName,
            mandatory: false,
            basicProperties: properties,
            body: body
        );
    }

    public async Task ReceiveMessagesAysync(Action<string> onMessageReceived)
    {
        var consumer = new AsyncEventingBasicConsumer(Channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            onMessageReceived(message);
            await Task.Yield(); // Allow asynchronous completion
        };
        await Channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
    }

    public async Task CloseAsync() => await Connection.CloseAsync();
}