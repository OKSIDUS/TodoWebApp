using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;

namespace TodoListApp.WebApi.Controllers;
public class TodoListController : Controller
{
    private readonly ITodoListService todoListService;

    public TodoListController(ITodoListService todoListService)
    {
        this.todoListService = todoListService;
    }

    public IActionResult Index()
    {
        return this.View(this.todoListService.GetTodoLists);
    }
}
