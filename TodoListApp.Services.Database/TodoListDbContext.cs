using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services.Database;
public class TodoListDbContext : DbContext
{
    public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
        : base(options)
    {
    }

    internal DbSet<TodoListEntity> TodoLists => this.Set<TodoListEntity>();
}
