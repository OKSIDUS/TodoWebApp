using TodoListApp.Services.Database.Entity;
using TodoListApp.Services.interfaces;

namespace TodoListApp.Services.Database.Services;
public class TaskDatabaseService : ITaskService
{
    private readonly TodoListDbContext context;

    public TaskDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }

    public void CreateTask(Task task)
    {
        _ = this.context.Tasks.Add(new TaskEntity
        {
            Title = task.Title,
            TodoListId = task.TodoListId,
            Status = task.Status,
        });

        _ = this.context.SaveChanges();
    }

    public void DeleteTask(int id)
    {
        var taskEntity = this.context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        if (taskEntity is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        _ = this.context.Tasks.Remove(taskEntity);
        _ = this.context.SaveChanges();
    }

    public IEnumerable<Task> GetAllTasks()
    {
        var taskEnity = this.context.Tasks.ToList();
        return taskEnity.Select(t => new Task
        {
            Id = t.Id,
            Title = t.Title,
            TodoListId = t.TodoListId,
            Status = t.Status,
            UserId = t.UserId,
        });
    }

    public Task GetTask(int id)
    {
        var task = this.context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        return new Task
        {
            Id = task.Id,
            Title = task.Title,
            Status = task.Status,
            TodoListId = task.TodoListId,
        };
    }

    public IEnumerable<Task> GetTasks(int todoListID)
    {
        var tasks = this.context.Tasks.Where(t => t.TodoListId == todoListID).ToList();
        return tasks.Select(t => new Task
        {
            Id = t.Id,
            Title = t.Title,
            TodoListId = t.TodoListId,
            Status = t.Status,
        });
    }

    public void ShareTask(int taskId, int userId)
    {
        var task = this.GetTask(taskId);
        if (this.context.Users.Where(u => u.Id == userId).FirstOrDefault() == null)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        _ = this.context.Tasks.Update(new TaskEntity
        {
            Id = taskId,
            Title = task.Title,
            Status = task.Status,
            TodoListId = task.TodoListId,
            UserId = userId,
        });

        _ = this.context.SaveChanges();
    }

    public void UpdateTask(Task task)
    {
        var taskEntity = this.context.Tasks.Where(t => t.Id == task.Id).FirstOrDefault();

        if (task != null)
        {
            taskEntity.Id = task.Id;
            taskEntity.Title = task.Title;
            taskEntity.Status = task.Status;
            taskEntity.TodoListId = task.TodoListId;
            taskEntity.UserId = task.UserId;

            _ = this.context.Tasks.Update(taskEntity);

            _ = this.context.SaveChanges();
        }
    }
}
