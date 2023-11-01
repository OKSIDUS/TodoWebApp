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

    [HttpGet("/Task")]
    public IActionResult Index(int todoListID)
    {
        var tasks = this.service.GetTasks(todoListID);
        var taskModel = tasks.Select(t => new TaskModel
        {
            Id = t.Id,
            Title = t.Title,
            TodoListId = t.TodoListId,
            IsDone = t.IsDone,
        });

        return this.Ok(taskModel);
    }

    [HttpPost("Task/Create")]
    public IActionResult Create(Task task)
    {
        this.service.CreateTask(task);
        return this.Ok(task);
    }

    [HttpDelete("Task/Delete")]
    public IActionResult Delete(int id)
    {
        this.service.DeleteTask(id);
        return this.Ok();
    }

    [HttpPut("Task/Update")]
    public IActionResult Update(Task task)
    {
        this.service.UpdateTask(task);
        return this.Ok();
    }
}
