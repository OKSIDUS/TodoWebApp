using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AutoMapper;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi;
public class TaskWebApiService : ITaskServiceAsync
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7071"),
    };

    private readonly IMapper mapper;

    public TaskWebApiService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<bool> CreateAsync(Task task)
    {
        var json = JsonSerializer.Serialize(this.mapper.Map<TaskModel>(task));

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("/Task/Create", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/Task/Delete/{id}");

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<Task> GetTaskAsync(int id)
    {
        var response = await httpClient.GetAsync($"/Task/{id}");

        if (response.IsSuccessStatusCode)
        {
            var task = await response.Content.ReadFromJsonAsync<Task>();
            return task;
        }

        return new Task();
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

    public async Task<bool> UpdateAsync(Task task)
    {
        var json = JsonSerializer.Serialize(this.mapper.Map<TaskModel>(task));

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync($"/Task/Update/{task.Id}", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}
