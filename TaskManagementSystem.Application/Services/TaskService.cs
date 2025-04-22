using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IServiceBusHelper _serviceBusHelper;
    private readonly IValidationService _validationService;

    public TaskService(ITaskRepository taskRepository, IServiceBusHelper serviceBusHelper, IValidationService validationService)
    {
        _taskRepository = taskRepository;
        _serviceBusHelper = serviceBusHelper;
        _validationService = validationService;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    public async Task<TaskItem> AddTaskAsync(TaskItemAddModel task)
    {
        _validationService.ValidateTaskItemAdd(task);

        var taskEntity = new TaskItem
        {
            Name = task.Name,
            Description = task.Description,
            Status = task.Status
        };

        await _taskRepository.AddAsync(taskEntity);

        // Send a message to the Service Bus if the task is completed
        await SendTaskMessageIfCompleted(taskEntity);

        return taskEntity;
    }

    public async Task<TaskItem> UpdateTaskStatusAsync(int id, TaskItemStatus status)
    {
        _validationService.ValidateUpdateTaskStatus(id, status);

        var task = await _taskRepository.GetByIdAsync(id);

        if (task == null)
        {
            throw new KeyNotFoundException($"Task with ID {id} not found.");
        }

        task.Status = status;
        await _taskRepository.UpdateAsync(task);

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

