using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Controllers;
public class TodoListController : Controller
{
    private readonly ITodoListServiceAsync service;

    public TodoListController(ITodoListServiceAsync service)
    {
        this.service = service;
    }

    public async Task<IActionResult> Index()
    {
        var todolists = await this.service.GetTodoListsAsync();
        var todolistsModel = todolists.Select(t => new TodoListModel
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
        });
        return this.View(todolistsModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var todoList = await this.service.GetTodoListAsync(id);

        if (todoList == null)
        {
            return this.NotFound();
        }

        return this.View(new TodoListModel
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Description = todoList.Description,

        });
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
        return this.View(new TodoListModel
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Description = todoList.Description,
        });
    }

    [HttpPost("/edit")]
    public async Task<IActionResult> Edit(TodoListModel todoList)
    {
        var result = await this.service.UpdateTodoListAsync(new TodoList
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Description = todoList.Description,
        });

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
    public async Task<IActionResult> Create(TodoListModel todoList)
    {
        var result = await this.service.CreateTodoListAsync(new TodoList
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Description = todoList.Description,
        });

        if (!result)
        {
            return this.BadRequest();
        }

        return this.RedirectToAction("Index");
    }
}
