using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi;
public class TodoListWebApiService : ITodoListServiceAsync
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7071"),
    };

    public Task CreateTodoListAsync(TodoList todoList)
    {
        throw new NotImplementedException();
    }

    public Task<TodoList> GetTodoListAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TodoList>> GetTodoListsAsync()
    {
        var response = await httpClient.GetAsync("/");

        if (response.IsSuccessStatusCode)
        {
            var todoLists = await response.Content.ReadFromJsonAsync<IEnumerable<TodoList>>();
            return todoLists;
        }

        return new List<TodoList>();
    }

    public Task RemoveTodoListAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTodoListAsync(TodoList todoList)
    {
        throw new NotImplementedException();
    }
}
