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

    public TodoList? GetTodoList(long id)
    {
        var todoListEntity = this.context.TodoLists.FirstOrDefault(x => x.Id == id);
        if (todoListEntity == null)
        {
            return null;
        }

        return new TodoList
        {
            Id = todoListEntity.Id,
            Title = todoListEntity.Title,
            Description = todoListEntity.Description,
            Tag = todoListEntity.Tag,
            TaskStatus = todoListEntity.TaskStatus,
        };
    }

    public void SaveTodoList(TodoList todoList)
    {
        throw new NotImplementedException();
    }
}
