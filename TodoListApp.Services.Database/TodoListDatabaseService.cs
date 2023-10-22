namespace TodoListApp.Services.Database;
public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext context;

    public TodoListDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }
    public IQueryable<TodoList> GetTodoLists => (IQueryable<TodoList>)this.context.TodoLists;

    public void CreateTodoList(TodoList todoList)
    {
        throw new NotImplementedException();
    }

    public void DeleteTodoList(TodoList todoList)
    {
        throw new NotImplementedException();
    }

    public void SaveTodoList(TodoList todoList)
    {
        throw new NotImplementedException();
    }
}
