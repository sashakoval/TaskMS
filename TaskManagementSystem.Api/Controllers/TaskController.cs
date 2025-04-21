using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        return new OkObjectResult(await _taskService.GetTasksAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(TaskItemAdd task)
    {
        return new OkObjectResult(await _taskService.AddTaskAsync(task));
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateTaskStatus(int id, TaskItemStatus status)
    {
        return new OkObjectResult(await _taskService.UpdateTaskStatusAsync(id, status));
    }
}
