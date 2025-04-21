namespace TaskManagementSystem.Application.Interfaces;

using TaskManagementSystem.Core.Entities;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetTasksAsync();
    Task<TaskItem> AddTaskAsync(TaskItemAdd task);
    Task<TaskItem> UpdateTaskStatusAsync(int id, TaskItemStatus status);
}
