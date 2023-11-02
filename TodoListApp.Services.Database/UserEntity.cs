using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Services.Database;
public class UserEntity
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public IEnumerable<TaskEntity> Tasks { get; set; }
}
