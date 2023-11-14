using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;
[Route("TodoList")]
public class TodoListController : Controller
{
    private readonly ITodoListService todoListService;

    private readonly IMapper mapper;

    public TodoListController(ITodoListService todoListService, IMapper mapper)
    {
        this.todoListService = todoListService;
        this.mapper = mapper;
    }

    [HttpGet("/")]
    public IActionResult GetAll()
    {
        var todoLists = this.todoListService.GetTodoLists();
        var todoList = todoLists.Select(task => this.mapper.Map<TodoListModel>(task));
        return this.Ok(todoList);
    }

    [HttpGet("/TodoLists/{id}")]
    public IActionResult GetTodoList(int id)
    {
        var todolist = this.todoListService.GetTodoList(id);
        var todoListModel = this.mapper.Map<TodoListModel>(todolist);
        return this.Ok(todoListModel);
    }

    [HttpPost("/TodoLists/Create")]
    public IActionResult Create([FromBody] TodoListModel todoList)
    {
        if (todoList == null)
        {
            return this.BadRequest(todoList);
        }
        else
        {
            var todo = this.mapper.Map<TodoList>(todoList);

            this.todoListService.CreateTodoList(todo);
            return this.Ok(todo);
        }
    }

    [HttpPost("/TodoLists/Update/{todoList.Id}")]
    public IActionResult Update([FromBody] TodoListModel? todoList)
    {
        if (todoList is null)
        {
            return this.BadRequest(todoList);
        }
        else
        {
            var todo = this.mapper.Map<TodoList>(todoList);

            this.todoListService.UpdateTodoList(todo);
            return this.Ok(todo);
        }
    }

    [HttpDelete("/TodoLists/Delete/{id}")]
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
