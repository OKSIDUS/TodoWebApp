namespace TodoListApp.Services;
public class Task
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int TodoListId { get; set; }

    public TaskStatus Status { get; set; }

    public int UserId { get; set; }
}
