using Microsoft.Extensions.DependencyInjection;
using OneWayChat.Application.Services;
using OneWayChat.Domain.Services;

namespace OneWayChat.Infrastructure;

public static class DependencyInjection
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        // Register RabbitMQService with a singleton lifetime
        services.AddSingleton<RabbitMqService>(provider =>
        {
            var rabbitService = new RabbitMqService("chat_queue");
            rabbitService.InitializeAsync("localhost").Wait(); // Ensure the service is initialized
            return rabbitService;
        });

        // Register IMessageService (Application Layer Service)
        services.AddSingleton<IMessageService, ChatService>();
    }
}
