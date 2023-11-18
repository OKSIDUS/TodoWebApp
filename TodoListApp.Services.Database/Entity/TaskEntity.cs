using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.Services.Database.Entity;
[Table("Task")]
public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int TodoListId { get; set; }

    public TaskStatus Status { get; set; }

    public string SharedFor { get; set; } = string.Empty;
}
