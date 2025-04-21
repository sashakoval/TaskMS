using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.Api.Handlers;

public class TaskHandler : BackgroundService
{
    private readonly IServiceBusHelper _serviceBusHelper;

    public TaskHandler(IServiceBusHelper serviceBusHelper)
    {
        _serviceBusHelper = serviceBusHelper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _serviceBusHelper.StartReceivingMessagesAsync(async message =>
        {
            Console.WriteLine($"Message has been handled: {message}");

            await Task.CompletedTask;
        });
    }
}
