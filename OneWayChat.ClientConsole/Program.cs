using Microsoft.Extensions.DependencyInjection;
using OneWayChat.Domain.Entities;
using OneWayChat.Domain.Services;
using OneWayChat.Infrastructure;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();  // Register services
        var serviceProvider = services.BuildServiceProvider();

        // Resolve services from the DI container
        var rabbitService = serviceProvider.GetRequiredService<RabbitMqService>();
        var messageService = serviceProvider.GetRequiredService<IMessageService>();

        // Init RabbitMQ
        await rabbitService.InitializeAsync("localhost");

        Console.WriteLine("Waiting for messages...");

        await rabbitService.ReceiveMessagesAysync(messageContent =>
        {
            var message = new Message(messageContent);
            messageService.ProcessMessage(message);
        });

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        await rabbitService.CloseAsync();
    }
}