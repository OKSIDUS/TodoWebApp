using System.Threading.Tasks;

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
                Title = task.Title,
                Description = task.Description,
                NumberOfTasks = task.NumberOfTasks,
                IsShared = task.IsShared,
            };
        }

        return new TodoList
        {
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
            Title = t.Title,
            Description = t.Description,
            NumberOfTasks = t.NumberOfTasks,
            IsShared = t.IsShared,
        });
    }

    public void RemoveTask(int id)
    {
        throw new NotImplementedException();
    }

    public TodoList UpdateTask(TodoList list)
    {
        throw new NotImplementedException();
    }
}
