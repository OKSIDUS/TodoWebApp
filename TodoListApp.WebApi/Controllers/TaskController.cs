using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;
using Task = TodoListApp.Services.Task;

namespace TodoListApp.WebApi.Controllers;
public class TaskController : Controller
{
    private readonly ITaskService service;

    public TaskController(ITaskService service)
    {
        this.service = service;
    }

    [HttpGet("/Task/{id}")]
    public IActionResult GetTask(int id)
    {
        var task = this.service.GetTask(id);
        return this.Ok(new TaskModel
        {
            Id = task.Id,
            Title = task.Title,
            TodoListId = task.TodoListId,
            Status = (Models.TaskStatus)task.Status,
            UserId = task.UserId,
        });
    }

    [HttpGet("Task/AllTasks")]
    public IActionResult GetAllTasks()
    {
        var tasks = this.service.GetAllTasks();
        return this.Ok(tasks);
    }

    [HttpGet("/Tasks/{todoListID}")]
    public IActionResult Index(int todoListID)
    {
        var tasks = this.service.GetTasks(todoListID);
        var taskModel = tasks.Select(t => new TaskModel
        {
            Id = t.Id,
            Title = t.Title,
            TodoListId = t.TodoListId,
            Status = (Models.TaskStatus)t.Status,
        });

        return this.Ok(taskModel);
    }

    [HttpPost("/Task/Create")]
    public IActionResult Create([FromBody] TaskModel task)
    {
        this.service.CreateTask(new Task
        {
            Id = task.Id,
            Title = task.Title,
            Status = (Services.TaskStatus)task.Status,
            TodoListId = task.TodoListId,
            UserId = task.UserId,
        });
        return this.Ok(task);
    }

    [HttpDelete("/Task/Delete/{id}")]
    public IActionResult Delete(int id)
    {
        this.service.DeleteTask(id);
        return this.Ok();
    }

    [HttpPost("/Task/Update/{task.Id}")]
    public IActionResult Update([FromBody] TaskModel task)
    {
        this.service.UpdateTask(new Task
        {
            Id = task.Id,
            Title = task.Title,
            Status = (Services.TaskStatus)task.Status,
            TodoListId = task.TodoListId,
            UserId = task.UserId,
        });
        return this.Ok();
    }

    [HttpPost("Task/Share")]
    public IActionResult Share(int taskId, int userId)
    {
        this.service.ShareTask(taskId, userId);
        return this.Ok();
    }
}
