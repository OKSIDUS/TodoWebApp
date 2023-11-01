namespace TodoListApp.Services.Database;
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
            IsDone = task.IsDone,
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

    public Task GetTask(int id)
    {
        var task = this.context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        return new Task
        {
            Id = task.Id,
            Title = task.Title,
            IsDone = task.IsDone,
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
            IsDone = t.IsDone,
        });
    }

    public void UpdateTask(Task task)
    {
        var taskEntity = this.context.Tasks.Where(t => t.Id == task.Id).FirstOrDefault();
        taskEntity.Title = task.Title;
        taskEntity.IsDone = task.IsDone;
        taskEntity.TodoListId = task.TodoListId;
        this.context.Tasks.Update(taskEntity);
        this.context.SaveChanges();
    }
}
