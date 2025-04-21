namespace TaskManagementSystem.Core.Entities;

public class TaskItemAdd
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public TaskItemStatus Status { get; set; }
}
