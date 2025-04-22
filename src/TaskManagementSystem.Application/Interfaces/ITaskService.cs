namespace TaskManagementSystem.Application.Interfaces;

using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Enums;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetTasksAsync();
    Task<TaskItem> AddTaskAsync(TaskItemAddModel task);
    Task<TaskItem> UpdateTaskStatusAsync(int id, TaskItemStatus status);
}
