using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.Infrastreucture.Helpers;

public class ServiceBusHelper : IServiceBusHelper
{
    private readonly ServiceBusClient _client;
    private readonly string _queueName;

    public ServiceBusHelper(IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("ServiceBus__ConnectionString") ?? throw new ArgumentException("Service Bus connection string is not configured.");
        _queueName = Environment.GetEnvironmentVariable("ServiceBus__QueueName") ?? throw new ArgumentException("Service Bus queue name is not configured.");

        _client = new ServiceBusClient(connectionString);
    }

    public async Task SendMessageAsync(string message)
    {
        var sender = _client.CreateSender(_queueName);
        await sender.SendMessageAsync(new ServiceBusMessage(message));
    }

    public async Task StartReceivingMessagesAsync(Func<string, Task> messageHandler)
    {
        var processor = _client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            var body = args.Message.Body.ToString();
            await messageHandler(body);
            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine($"Error: {args.Exception.Message}");
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }
}
