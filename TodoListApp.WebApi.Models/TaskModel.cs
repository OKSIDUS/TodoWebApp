namespace TodoListApp.WebApi.Models;
public class TaskModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int TodoListId { get; set; }

    public TaskStatus Status { get; set; }
}
