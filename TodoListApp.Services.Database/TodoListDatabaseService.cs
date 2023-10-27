namespace TodoListApp.Services.Database;
public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext dbContext;

    public TodoListDatabaseService(TodoListDbContext dbContext)
    {
        this.dbContext = dbContext;
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
}
