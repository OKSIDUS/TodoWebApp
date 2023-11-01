using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;

namespace TodoListApp.WebApp.Controllers;
public class TodoListController : Controller
{
    private readonly ITodoListService _service;

    public TodoListController(ITodoListService service)
    {
        this._service = service;
    }
}
