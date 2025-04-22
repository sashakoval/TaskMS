using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Application.Models;

public class TaskItemAddModel
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public TaskItemStatus Status { get; set; }
}
