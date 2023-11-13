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
        var todoListId = this.Request.Query["id"];
        this.ViewBag.TodoListId = todoListId;
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
        this.ViewBag.TodoListId = id;
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
            return this.RedirectToAction("Index", new { id = task.TodoListId });
        }

        return this.BadRequest();
    }

    public async Task<IActionResult> DetailsTask(int id)
    {
        var task = await this.service.GetTaskAsync(id);
        if (task is null)
        {
            return this.BadRequest();
        }

        return this.View(new TaskModel
        {
            Title = task.Title,
            Id = task.Id,
            TodoListId = task.TodoListId,
            Status = (WebApi.Models.TaskStatus)task.Status,
            UserId = task.UserId,
        });
    }

    public async Task<IActionResult> DeleteTask(int id, int todoListId)
    {
        var result = await this.service.DeleteAsync(id);

        if (result)
        {
            return this.RedirectToAction("Index", new { id = todoListId });
        }

        return this.BadRequest();
    }


    [HttpGet("/Task/Edit")]
    public async Task<IActionResult> EditTask(int id)
    {
        var task = await this.service.GetTaskAsync(id);
        return this.View(new TaskModel
        {
            Id = task.Id,
            Title = task.Title,
            TodoListId = task.TodoListId,
            Status = (WebApi.Models.TaskStatus)task.Status,
            UserId = task.UserId,
        });
    }

    [HttpPost("/Task/Edit")]
    public async Task<IActionResult> EditTask(TaskModel task)
    {
        var result = await this.service.UpdateAsync(new Services.Task
        {
            Id = task.Id,
            Title = task.Title,
            UserId = task.UserId,
            TodoListId = task.TodoListId,
            Status = (Services.TaskStatus)task.Status,
        });

        if (!result)
        {
            return this.BadRequest();
        }

        return this.RedirectToAction("Index", new { id = task.TodoListId });
    }
}
