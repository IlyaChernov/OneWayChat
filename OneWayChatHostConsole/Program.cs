using Microsoft.Extensions.DependencyInjection;
using OneWayChat.Domain.Entities;
using OneWayChat.Infrastructure;

class Program
{
    static async Task Main(string[] args)
    {        
        var services = new ServiceCollection();
        services.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        // Get the RabbitMQ service
        var rabbitService = serviceProvider.GetRequiredService<RabbitMqService>();
      
        Console.WriteLine("Enter a message to send (type 'exit' to quit):");

        string? input;
        while (!string.IsNullOrEmpty(input = Console.ReadLine()) && input != "exit")
        {
            var message = new Message(input);
            await rabbitService.SendMessageAsync(message);
            Console.WriteLine("Message sent.");
        }

        await rabbitService.CloseAsync();
    }
}
