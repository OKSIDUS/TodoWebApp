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

    [HttpGet("/")]
    public IActionResult Index()
    {
        var todoLists = this.todoListService.GetTodoLists();
        return this.Ok(todoLists);
    }

    [HttpPost("create")]
    public IActionResult Create(TodoList todoList)
    {
        if (todoList == null)
        {
            return this.BadRequest(todoList);
        }
        else
        {
            this.todoListService.CreateTask(todoList);
            return this.Ok(todoList);
        }
    }
}
