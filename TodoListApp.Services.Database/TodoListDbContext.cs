using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entity;

namespace TodoListApp.Services.Database;
public class TodoListDbContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
