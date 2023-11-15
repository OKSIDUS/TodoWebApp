using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Controllers;
public class TodoListController : Controller
{
    private readonly ITodoListServiceAsync service;
    private readonly IMapper mapper;

    public TodoListController(ITodoListServiceAsync service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var todolists = await this.service.GetTodoListsAsync();
        var todolistsModel = todolists.Select(t => this.mapper.Map<TodoListModel>(t));
        return this.View(todolistsModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var todoList = await this.service.GetTodoListAsync(id);

        if (todoList == null)
        {
            return this.NotFound();
        }

        return this.View(this.mapper.Map<TodoListModel>(todoList));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await this.service.RemoveTodoListAsync(id);

        if (result)
        {
            return this.RedirectToAction("Index");
        }
        else
        {
            return this.NotFound();
        }
    }

    [HttpGet("/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var todoList = await this.service.GetTodoListAsync(id);
        return this.View(this.mapper.Map<TodoListModel>(todoList));
    }

    [HttpPost("/edit")]
    public async Task<IActionResult> Edit(TodoListModel? todoList)
    {
        if (todoList is null)
        {
            return this.BadRequest();
        }

        var result = await this.service.UpdateTodoListAsync(this.mapper.Map<TodoList>(todoList));

        if (!result)
        {
            return this.BadRequest();
        }

        return this.View(todoList);
    }

    [HttpGet("/create")]
    public IActionResult Create()
    {
        return this.View();
    }

    [HttpPost("/create")]
    public async Task<IActionResult> Create(TodoListModel? todoList)
    {
        if (todoList is null)
        {
            return this.BadRequest();
        }

        var result = await this.service.CreateTodoListAsync(this.mapper.Map<TodoList>(todoList));

        if (!result)
        {
            return this.BadRequest();
        }

        return this.RedirectToAction("Index");
    }
}
