using System.Net.Http.Json;

namespace TodoListApp.Services.WebApi;
public class TaskWebApiService : ITaskServiceAsync
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7071"),
    };

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
