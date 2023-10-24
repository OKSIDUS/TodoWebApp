using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;

[Route("api/todolists")]
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
    [Route("")]
    public IActionResult GetTodoLists()
    {
        return this.View(this.todoListService.GetTodoLists);
    }

    [HttpGet("{id}")]
    [Route("{id}")]
    public IActionResult GetTodoList(int id)
    {
        TodoList? todoList = this.todoListService.GetTodoList(id);
        if (todoList is null)
        {
            return this.NotFound();
        }

        return this.View(todoList);
    }

    [HttpPost]
    [Route("")]
    public IActionResult CreateTodoList(TodoList todoList)
    {
        this.todoListService.CreateTodoList(todoList);
        return this.CreatedAtAction("GetTodoList", new { id = todoList.Id }, todoList);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateTodoList(TodoList todoList)
    {
        this.todoListService.UpdateTodoList(todoList);
        return this.Ok(todoList);
    }

    [HttpDelete("{id}")]
    [Route("{id}")]
    public IActionResult DeleteTodoList(int id)
    {
        var deleted = this.todoListService.GetTodoList(id);
        if (deleted is null)
        {
            return this.NotFound();
        }

        this.todoListService.DeleteTodoList(deleted);
        return this.NoContent();
    }
}
