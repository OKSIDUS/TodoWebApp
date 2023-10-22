using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;
public class TodoListController : Controller
{
    private readonly ITodoListService todoListService;

    public TodoListController(ITodoListService todoListService)
    {
        this.todoListService = todoListService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IQueryable<TodoListModel> todoLists = (IQueryable<TodoListModel>)this.todoListService.GetTodoLists;
        return this.View(todoLists);
    }

    [HttpGet]
    public IActionResult GetTodoLists()
    {
        return this.View(this.todoListService.GetTodoLists);
    }

    [HttpGet("{id}")]
    public IActionResult GetTodoList(int id)
    {
        TodoList? todoList = this.todoListService.GetTodoList(id);
        if (todoList is null)
        {
            return this.NotFound();
        }

        return this.View(todoList);
    }
}
