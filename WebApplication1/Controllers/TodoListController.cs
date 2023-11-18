using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace WebApplication1.Controllers;
public class TodoListController : Controller
{
    private readonly ITodoListServiceAsync service;
    private readonly IMapper mapper;
    private readonly UserManager<IdentityUser> manager;

    public TodoListController(ITodoListServiceAsync service, IMapper mapper, UserManager<IdentityUser> manager)
    {
        this.service = service;
        this.mapper = mapper;
        this.manager = manager;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await this.manager.GetUserAsync(this.User);
        var todolists = await this.service.GetTodoListsAsync(user.UserName);
        var todolistsModel = todolists.Select(t => this.mapper.Map<TodoListModel>(t));
        return this.View(todolistsModel);
    }


    public async Task<IActionResult> Details(int id)
    {
        var todoList = this.mapper.Map<TodoListModel>(await this.service.GetTodoListAsync(id));
        if (todoList != null)
        {
            return this.View(todoList);
        }

        return this.BadRequest();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TodoListModel todoList)
    {
        var user = await this.manager.GetUserAsync(this.User);
        todoList.CreatedBy = user.UserName;
        var result = await this.service.CreateTodoListAsync(this.mapper.Map<TodoList>(todoList));
        if (result)
        {
            return this.RedirectToAction("Index");
        }

        return this.BadRequest();
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await this.service.RemoveTodoListAsync(id);

        if (result)
        {
            return this.RedirectToAction("Index");
        }

        return this.BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var todoList = await this.service.GetTodoListAsync(id);
        return this.View(this.mapper.Map<TodoListModel>(todoList));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TodoListModel todoList)
    {
        var result = await this.service.UpdateTodoListAsync(this.mapper.Map<TodoList>(todoList));
        if (result)
        {
            return this.RedirectToAction("Index");
        }

        return this.BadRequest();
    }

}
