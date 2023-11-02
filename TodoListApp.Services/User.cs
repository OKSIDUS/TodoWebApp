namespace TodoListApp.Services;
public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public IEnumerable<Task> Tasks { get; set; }
}
