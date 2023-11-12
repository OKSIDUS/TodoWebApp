using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Controllers;
public class TaskController : Controller
{
    private readonly ITaskServiceAsync service;

    public TaskController(ITaskServiceAsync service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int id)
    {
        var tasks = await this.service.GetTasksAsync(id);

        var tasksModel = tasks.Select(t => new TaskModel
        {
            Id = t.Id,
            Title = t.Title,
            TodoListId = t.TodoListId,
            UserId = t.UserId,
            Status = (WebApi.Models.TaskStatus)t.Status,
        });

        return this.View(tasksModel);
    }
}
