using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi;
public class TodoListWebApiService : ITodoListServiceAsync
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7071"),
    };

    public async Task<bool> CreateTodoListAsync(TodoList todoList)
    {
        var json = JsonSerializer.Serialize(new TodoListModel
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Description = todoList.Description,
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/TodoLists/Create", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new InvalidOperationException(nameof(todoList));
        }

    }

    public async Task<TodoList> GetTodoListAsync(int id)
    {
        var response = await httpClient.GetAsync($"/TodoLists/{id}");

        if (response.IsSuccessStatusCode)
        {
            var todoList = await response.Content.ReadFromJsonAsync<TodoList>();
            return todoList;
        }

        return new TodoList();
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

    public async Task<bool> RemoveTodoListAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/TodoLists/Delete/{id}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new InvalidOperationException(nameof(id));
        }
    }

    public async Task<bool> UpdateTodoListAsync(TodoList todoList)
    {
        var json = JsonSerializer.Serialize(new TodoListModel
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Description = todoList.Description,
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var repsonse = await httpClient.PostAsync($"/TodoLists/Update/{todoList.Id}", content);

        if (repsonse.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
