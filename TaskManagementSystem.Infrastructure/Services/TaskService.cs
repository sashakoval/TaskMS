using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Infrastructure.Helpers;

namespace TaskManagementSystem.Infrastructure.Data;

public class TaskService : ITaskService
{
	private readonly TaskDbContext _context;
    private readonly IServiceBusHelper _serviceBusHelper;

    public TaskService(TaskDbContext context, IServiceBusHelper serviceBusHelper)
    {
        _context = context;
        _serviceBusHelper = serviceBusHelper;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksAsync()
	{
		return await _context.Tasks.ToListAsync();
	}

    public async Task<TaskItem> AddTaskAsync(TaskItemAdd task)
    {
        ValidationHelper.ValidateTaskItemAdd(task);

        var taskEntity = new TaskItem
        {
            Name = task.Name,
            Description = task.Description,
            Status = task.Status
        };

        _context.Tasks.Add(taskEntity);
        await _context.SaveChangesAsync();

        // Send a message to the Service Bus if the task is completed
        await SendTaskMessageIfCompleted(taskEntity);

        return taskEntity;
    }

    public async Task<TaskItem> UpdateTaskStatusAsync(int id, TaskItemStatus status)
    {
        ValidationHelper.ValidateUpdateTaskStatusAsync(id, status);

        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            throw new KeyNotFoundException($"Task with ID {id} not found.");
        }

        task.Status = status;
        await _context.SaveChangesAsync();

        // Send a message to the Service Bus if the task is completed
        await SendTaskMessageIfCompleted(task);

        return task;
    }

    private async Task SendTaskMessageIfCompleted(TaskItem task)
    {
        if (task.Status == TaskItemStatus.Completed)
        {
            try
            {
                var message = $"Task with ID {task.Id} has been completed.";
                await _serviceBusHelper.SendMessageAsync(message);
                Console.WriteLine($"Message sent to Service Bus: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message to Service Bus: {ex.Message}");
            }
        }
    }
}
