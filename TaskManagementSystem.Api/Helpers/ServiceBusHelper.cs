using Azure.Messaging.ServiceBus;
using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.Services;

public class ServiceBusHelper : IServiceBusHelper
{
    private readonly ServiceBusClient _client;
    private readonly string _queueName;

    public ServiceBusHelper(IConfiguration configuration)
    {
        var connectionString = configuration["ServiceBus:ConnectionString"] ?? throw new ArgumentNullException("ServiceBus:ConnectionString");
        _queueName = configuration["ServiceBus:QueueName"] ?? throw new ArgumentNullException("ServiceBus:QueueName");
        _client = new ServiceBusClient(connectionString);
    }

    // Send a message to the Service Bus queue  
    public async Task SendMessageAsync(string message)
    {
        var sender = _client.CreateSender(_queueName);
        await sender.SendMessageAsync(new ServiceBusMessage(message));
    }

    // Start receiving messages from the Service Bus queue  
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
