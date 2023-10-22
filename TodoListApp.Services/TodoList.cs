namespace TodoListApp.Services;
public class TodoList
{
    public long Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Tag { get; set; }

    public bool TaskStatus { get; set; }
}
