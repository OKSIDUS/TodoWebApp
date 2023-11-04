using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.Services.Database.Entity;
[Table("TodoList")]
public class TodoListEntity
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}
