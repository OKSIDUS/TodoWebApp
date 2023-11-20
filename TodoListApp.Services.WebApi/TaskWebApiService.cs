using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using AutoMapper;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi;
public class TaskWebApiService : ITaskServiceAsync
{
    private static readonly HttpClient HttpClient = new ()
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        BaseAddress = new Uri("https://localhost:7071"),
#pragma warning restore S1075 // URIs should not be hardcoded
    };

    private readonly IMapper mapper;

    public TaskWebApiService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Task>> AssignedTasks(string sharedFor)
    {
        var response = await HttpClient.GetAsync($"/Task/AssignedTasks/{sharedFor}");
        if (response.IsSuccessStatusCode)
        {
            var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<Task>>();
            if (tasks != null)
            {
                return tasks;
            }
        }

        return new List<Task>();
    }

    public async Task<bool> CreateAsync(Task? task)
    {
        if (task is null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize(this.mapper.Map<TaskModel>(task));

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync("/Task/Create", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"/Task/Delete/{id}");

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<Task> GetTaskAsync(int id)
    {
        var response = await HttpClient.GetAsync($"/Task/{id}");

        if (response.IsSuccessStatusCode)
        {
            var task = await response.Content.ReadFromJsonAsync<Task>();
            if (task != null)
            {
                return task;
            }
        }

        return new Task();
    }

    public async Task<IEnumerable<Task>> GetTasksAsync(int todoListID)
    {
        var response = await HttpClient.GetAsync($"/Tasks/{todoListID}");

        if (response.IsSuccessStatusCode)
        {
            var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<Task>>();
            if (tasks != null)
            {
                return tasks;
            }
        }

        return new List<Task>();
    }

    public async Task<bool> UpdateAsync(Task? task)
    {
        if (task is null)
        {
            return false;
        }

        var json = JsonSerializer.Serialize(this.mapper.Map<TaskModel>(task));

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await HttpClient.PostAsync($"/Task/Update/{task.Id}", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}
