using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Application.Interfaces;

public interface IValidationService
{
    void ValidateTaskItemAdd(TaskItemAddModel task);
    void ValidateUpdateTaskStatus(int id, TaskItemStatus status);
}

