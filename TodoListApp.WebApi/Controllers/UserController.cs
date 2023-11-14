using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;
public class UserController : Controller
{
    private readonly IUserService service;
    private readonly IMapper mapper;

    public UserController(IUserService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
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
        this.service.CreateUser(this.mapper.Map<User>(user));
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
        return this.Ok(this.mapper.Map<UserModel>(user));
    }

    [HttpPut("User/Update")]
    public IActionResult UpdateUser(UserModel user)
    {
        this.service.UpdateUser(this.mapper.Map<User>(user));
        return this.Ok();
    }
}
