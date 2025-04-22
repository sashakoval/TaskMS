using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Api.Endpoints;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tasks", async (ITaskService taskService) =>
        {
            var tasks = await taskService.GetTasksAsync();
            return Results.Ok(tasks);
        });

        app.MapPost("/api/tasks", async (TaskItemAddModel task, ITaskService taskService) =>
        {
            var createdTask = await taskService.AddTaskAsync(task);
            return Results.Ok(createdTask);
        });

        app.MapPut("/api/tasks/{id}/status", async (int id, TaskItemStatus status, ITaskService taskService) =>
        {
            var updatedTask = await taskService.UpdateTaskStatusAsync(id, status);
            return Results.Ok(updatedTask);
        });
    }
}

