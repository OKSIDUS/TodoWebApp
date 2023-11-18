using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entity;

namespace TodoListApp.Services.Database;
public class TodoListDbContext : DbContext
{
    public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoListEntity> TodoLists { get; set; }

    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoListEntity>()
            .HasMany<TaskEntity>()
            .WithOne()
            .HasForeignKey(e => e.TodoListId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
