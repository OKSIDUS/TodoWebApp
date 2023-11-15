using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.Services.Database.Entity;
[Table("User")]
public class UserEntity
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
