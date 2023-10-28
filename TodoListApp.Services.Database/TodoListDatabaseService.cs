namespace TodoListApp.Services.Database;
public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext dbContext;

    public TodoListDatabaseService(TodoListDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void CreateTask(TodoList task)
    {
        var entity = new TodoListEntity
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            NumberOfTasks = task.NumberOfTasks,
            IsShared = task.IsShared,
        };
        _ = this.dbContext.Add(entity);
        _ = this.dbContext.SaveChanges();
    }

    public TodoList GetTask(int id)
    {
        var task = this.dbContext.TodoLists.Where(task => task.Id == id).FirstOrDefault();

        if (task != null)
        {
            return new TodoList
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                NumberOfTasks = task.NumberOfTasks,
                IsShared = task.IsShared,
            };
        }

        return new TodoList
        {
            Id = id,
            Title = string.Empty,
            Description = string.Empty,
            NumberOfTasks = 0,
            IsShared = false,
        };
    }

    public IEnumerable<TodoList> GetTodoLists()
    {
        var todoLists = this.dbContext.TodoLists.ToList();
        return todoLists.Select(t => new TodoList
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            NumberOfTasks = t.NumberOfTasks,
            IsShared = t.IsShared,
        });
    }

    public void RemoveTask(int id)
    {
        var taskEntity = this.dbContext.TodoLists.Where(t => t.Id == id).FirstOrDefault();
        if (taskEntity != null)
        {
            _ = this.dbContext.TodoLists.Remove(taskEntity);
            _ = this.dbContext.SaveChanges();
        }
    }

    public void UpdateTask(TodoList task)
    {
        if (task is null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        if (task.Id == 0)
        {
            this.CreateTask(task);
        }
        else
        {
            var taskEntity = this.dbContext.TodoLists.Where(t => t.Id == task.Id).FirstOrDefault();
            taskEntity.Title = task.Title;
            taskEntity.Description = task.Description;
            taskEntity.NumberOfTasks = task.NumberOfTasks;
            taskEntity.IsShared = task.IsShared;

            _ = this.dbContext.Update(taskEntity);

            _ = this.dbContext.SaveChanges();
        }
    }
}
