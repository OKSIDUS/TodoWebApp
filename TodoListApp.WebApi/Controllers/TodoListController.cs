using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;
[Route("TodoList")]
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
        var todoList = todoLists.Select(task => new TodoList
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
        });
        return this.Ok(todoList);
    }

    [HttpGet("/id")]
    public IActionResult GetTodoList(int id)
    {
        var todolist = this.todoListService.GetTodoList(id);
        var todoListModel = new TodoList
        {
            Id = id,
            Title = todolist.Title,
            Description = todolist.Description,
        };
        return this.Ok(todoListModel);
    }

    [HttpPost("create")]
    public IActionResult Create(TodoListModel todoList)
    {
        if (todoList == null)
        {
            return this.BadRequest(todoList);
        }
        else
        {
            var todo = new TodoList
            {
                Id = todoList.Id,
                Title = todoList.Title,
                Description = todoList.Description,
            };

            this.todoListService.CreateTodoList(todo);
            return this.Ok(todo);
        }
    }

    [HttpPut("update")]
    public IActionResult Update(TodoListModel? todoList)
    {
        if (todoList is null)
        {
            return this.BadRequest(todoList);
        }
        else
        {
            var todo = new TodoList
            {
                Id = todoList.Id,
                Title = todoList.Title,
                Description = todoList.Description,
            };

            this.todoListService.UpdateTodoList(todo);
            return this.Ok(todo);
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
