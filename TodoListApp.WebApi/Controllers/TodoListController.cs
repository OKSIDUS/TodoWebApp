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

    [HttpGet("/")]
    public IActionResult Index()
    {
        var todoLists = this.todoListService.GetTodoLists();
        var todoListModel = todoLists.Select(task => new TodoListModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
        });
        return this.Ok(todoListModel);
    }

    [HttpGet("/id")]
    public IActionResult GetTodoList(int id)
    {
        var todolist = this.todoListService.GetTodoList(id);
        var todoListModel = new TodoListModel
        {
            Id = id,
            Title = todolist.Title,
            Description = todolist.Description,
        };
        return this.Ok(todoListModel);
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
            this.todoListService.CreateTodoList(todoList);
            return this.Ok(new TodoListModel
            {
                Id = todoList.Id,
                Title = todoList.Title,
                Description = todoList.Description,
            });
        }
    }

    [HttpPut("update")]
    public IActionResult Update(TodoList? todoList)
    {
        if (todoList is null)
        {
            return this.BadRequest(todoList);
        }
        else
        {
            this.todoListService.UpdateTodoList(todoList);
            return this.Ok(new TodoListModel
            {
                Id = todoList.Id,
                Title = todoList.Title,
                Description = todoList.Description,
            });
        }
    }

    [HttpDelete("delete")]
    public IActionResult Delete(int id)
    {
        if (id <= 0)
        {
            return this.BadRequest();
        }
        else
        {
            this.todoListService.RemoveTodoList(id);
            return this.Ok();
        }
    }
}
