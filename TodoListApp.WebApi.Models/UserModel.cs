namespace TodoListApp.WebApi.Models;
public class UserModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
