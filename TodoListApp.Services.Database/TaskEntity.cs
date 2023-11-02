using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.Services.Database;
[Table("Task")]
public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public int TodoListId { get; set; }

    public TaskStatus Status { get; set; }
}
