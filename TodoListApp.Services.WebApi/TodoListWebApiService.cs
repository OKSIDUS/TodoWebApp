using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AutoMapper;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi;
public class TodoListWebApiService : ITodoListServiceAsync
{
    private static readonly HttpClient HttpClient = new ()
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        BaseAddress = new Uri("https://localhost:7071"),
#pragma warning restore S1075 // URIs should not be hardcoded
    };

    private readonly IMapper mapper;

    public TodoListWebApiService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<bool> CreateTodoListAsync(TodoList? todoList)
    {
        if (todoList is null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize(this.mapper.Map<TodoListModel>(todoList));

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync("/TodoLists/Create", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<TodoList?> GetTodoListAsync(int id)
    {
        var response = await HttpClient.GetAsync($"/TodoLists/{id}");

        if (response.IsSuccessStatusCode)
        {
            var todoList = await response.Content.ReadFromJsonAsync<TodoList>();
            if (todoList != null)
            {
                return todoList;
            }
        }

        return null;
    }

    public async Task<IEnumerable<TodoList>> GetTodoListsAsync(string createdBy)
    {
        var response = await HttpClient.GetAsync($"/{createdBy}");

        if (response.IsSuccessStatusCode)
        {
            var todoLists = await response.Content.ReadFromJsonAsync<IEnumerable<TodoList>>();
            if (todoLists != null)
            {
                return todoLists;
            }
        }

        return new List<TodoList>();
    }

    public async Task<bool> RemoveTodoListAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"/TodoLists/Delete/{id}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateTodoListAsync(TodoList todoList)
    {
        if (todoList is null)
        {
            return false;

            // return true; ????
        }

        var json = JsonSerializer.Serialize(this.mapper.Map<TodoListModel>(todoList));

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var repsonse = await HttpClient.PostAsync($"/TodoLists/Update/{todoList.Id}", content);

        if (repsonse.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}
