using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Controllers;
public class TodoListController : Controller
{
    private readonly ITodoListServiceAsync _service;

    public TodoListController(ITodoListServiceAsync service)
    {
        this._service = service;
    }

    public async Task<IActionResult> Index()
    {
        var todolists = await this._service.GetTodoListsAsync();
        var todolistsModel = todolists.Select(t => new TodoListModel
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
        });
        return this.View(todolistsModel);

    }
}
