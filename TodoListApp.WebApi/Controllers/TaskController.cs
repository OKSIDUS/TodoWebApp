using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;
using Task = TodoListApp.Services.Task;

namespace TodoListApp.WebApi.Controllers;
public class TaskController : Controller
{
    private readonly ITaskService service;
    private readonly IMapper mapper;

    public TaskController(ITaskService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet("/Task/{id}")]
    public IActionResult GetTask(int id)
    {
        var task = this.service.GetTask(id);
        return this.Ok(this.mapper.Map<TaskModel>(task));
    }

    [HttpGet("Task/AllTasks")]
    public IActionResult GetAllTasks()
    {
        var tasks = this.service.GetAllTasks();
        return this.Ok(this.mapper.Map<TaskModel>(tasks));
    }

    [HttpGet("/Tasks/{todoListID}")]
    public IActionResult Index(int todoListID)
    {
        var tasks = this.service.GetTasks(todoListID);
        var taskModel = tasks.Select(t => this.mapper.Map<TaskModel>(t));

        return this.Ok(taskModel);
    }

    [HttpPost("/Task/Create")]
    public IActionResult Create([FromBody] TaskModel task)
    {
        this.service.CreateTask(this.mapper.Map<Task>(task));
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
        this.service.UpdateTask(this.mapper.Map<Task>(task));
        return this.Ok();
    }

    [HttpPost("Task/Share")]
    public IActionResult Share(int taskId, int userId)
    {
        this.service.ShareTask(taskId, userId);
        return this.Ok();
    }
}
