using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi;
public class TaskWebApiService : ITaskServiceAsync
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7071"),
    };

    public async Task<bool> CreateAsync(Task task)
    {
        var json = JsonSerializer.Serialize(new TaskModel
        {
            Title = task.Title,
            UserId = task.UserId,
            Status = (TodoListApp.WebApi.Models.TaskStatus)task.Status,
            TodoListId = task.TodoListId,
        });

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/Task/Create", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Task>> GetTasksAsync(int todoListID)
    {
        var response = await httpClient.GetAsync($"/Tasks/{todoListID}");

        if (response.IsSuccessStatusCode)
        {
            var task = await response.Content.ReadFromJsonAsync<IEnumerable<Task>>();
            return task;
        }

        return new List<Task>();
    }
}
