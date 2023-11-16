using System.Net.Http.Json;
using AutoMapper;
using TodoListApp.Services.interfaces;

namespace TodoListApp.Services.WebApi;
public class UserWebApiService : IUserServiceAsync
{
    private static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7071"),
    };

    private readonly IMapper mapper;

    public UserWebApiService(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public async Task<User?> CheckUser(string name, string password)
    {
        var response = await HttpClient.GetAsync($"User/Check/{name}/{password}");

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User?>();
            if (user != null)
            {
                return user;
            }
        }

        return null;
    }
}
