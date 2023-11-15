using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Controllers;
public class TaskController : Controller
{
    private readonly ITaskServiceAsync service;
    private readonly IMapper mapper;

    public TaskController(ITaskServiceAsync service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int id)
    {
        var todoListId = this.Request.Query["id"];
        this.ViewBag.TodoListId = todoListId;
        var tasks = await this.service.GetTasksAsync(id);

        var tasksModel = tasks.Select(t => this.mapper.Map<TaskModel>(t));

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
        if (task is null)
        {
            return this.BadRequest();
        }

        var result = await this.service.CreateAsync(this.mapper.Map<Services.Task>(task));

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

        return this.View(this.mapper.Map<TaskModel>(task));
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
        return this.View(this.mapper.Map<TaskModel>(task));
    }

    [HttpPost("/Task/Edit")]
    public async Task<IActionResult> EditTask(TaskModel task)
    {
        if (task is null)
        {
            return this.BadRequest();
        }

        var result = await this.service.UpdateAsync(this.mapper.Map<Services.Task>(task));

        if (!result)
        {
            return this.BadRequest();
        }

        return this.RedirectToAction("Index", new { id = task.TodoListId });
    }
}
