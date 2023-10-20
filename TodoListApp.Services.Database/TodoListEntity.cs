using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Services.Database;
internal class TodoListEntity
{
    public long NumberOfTask { get; set; }

    [Required]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Tag { get; set; }

    public bool TaskStatus { get; set; }
}
