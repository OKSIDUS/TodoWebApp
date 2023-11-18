namespace TodoListApp.WebApi.Models;
public class TaskModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int TodoListId { get; set; }

    public TaskStatus Status { get; set; }

    public string SharedFor { get; set; } = string.Empty;
}
