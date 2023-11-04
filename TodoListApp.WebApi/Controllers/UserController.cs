using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;
public class UserController : Controller
{
    private readonly IUserService service;

    public UserController(IUserService service)
    {
        this.service = service;
    }

    [HttpGet("GetUserTasks")]
    public IActionResult GetUserTasks(int userId)
    {
        var tasks = this.service.GetUserTasks(userId);
        return this.Ok(tasks);
    }

    [HttpPost("CreateUser")]
    public IActionResult Create(UserModel user)
    {
        this.service.CreateUser(new User
        {
            Id = user.Id,
            Name = user.Name,
            Password = user.Password,
        });
        return this.Ok();
    }

    [HttpGet("GetUser")]
    public IActionResult GetUser(int userId)
    {
        var user = this.service.GetUser(userId);
        return this.Ok(user);
    }

    [HttpGet("GetUsers")]
    public IActionResult GetUsers()
    {
        var user = this.service.GetUsers();
        return this.Ok(user);
    }

    [HttpPut("User/Update")]
    public IActionResult UpdateUser(UserModel user)
    {
        this.service.UpdateUser(new User
        {
            Id = user.Id,
            Name = user.Name,
            Password = user.Password,
        });
        return this.Ok();
    }
}
