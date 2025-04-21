namespace TaskManagementSystem.Core.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public TaskItemStatus Status { get; set; }
}
