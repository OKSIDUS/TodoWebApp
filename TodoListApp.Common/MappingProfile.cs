using AutoMapper;
using TodoListApp.Services;
using TodoListApp.Services.Database.Entity;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Common;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<TodoList, TodoListEntity>().ReverseMap();
        this.CreateMap<TodoList, TodoListModel>().ReverseMap();
        this.CreateMap<Services.TaskStatus, WebApi.Models.TaskStatus>().ReverseMap();
        this.CreateMap<Services.Task, TaskEntity>().ReverseMap();
        this.CreateMap<Services.Task, TaskModel>().ReverseMap();
        this.CreateMap<User, UserEntity>().ReverseMap();
        this.CreateMap<User, UserModel>().ReverseMap();
    }
}
