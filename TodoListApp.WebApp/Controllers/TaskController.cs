using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
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

    [HttpGet]
    [Route("/Task/CreateTask")]
    public IActionResult CreateTask(int id)
    {
        ViewBag.TodoListId = id;
        return this.View();
    }

    [HttpPost]
    [Route("/Task/CreateTask")]
    public async Task<IActionResult> CreateTask(TaskModel task)
    {
        var result = await this.service.CreateAsync(new Services.Task
        {
            Title = task.Title,
            TodoListId = task.TodoListId,
            UserId = 0,
            Status = Services.TaskStatus.Active,
        });

        if (result)
        {
            return this.View(task);
        }

        return this.BadRequest();
    }
}
