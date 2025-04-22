using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Application.Services;

public class ValidationService : IValidationService
{
    public void ValidateTaskItemAdd(TaskItemAddModel task)
    {
        if (string.IsNullOrWhiteSpace(task.Name))
        {
            throw new ArgumentException("Task name is required.");
        }

        if (string.IsNullOrWhiteSpace(task.Description))
        {
            throw new ArgumentException("Task description is required.");
        }

        if (!Enum.IsDefined(typeof(TaskItemStatus), task.Status))
        {
            throw new ArgumentException("Invalid task status.");
        }
    }

    public void ValidateUpdateTaskStatus(int id, TaskItemStatus status)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid task ID.");
        }

        if (!Enum.IsDefined(typeof(TaskItemStatus), status))
        {
            throw new ArgumentException("Invalid task status.");
        }
    }
}

