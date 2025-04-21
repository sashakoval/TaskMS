namespace TaskManagementSystem.Application.Interfaces;

public interface IServiceBusHelper
{
    Task SendMessageAsync(string message);

    Task StartReceivingMessagesAsync(Func<string, Task> messageHandler);
}
