namespace TodoListApp.Services.Database;
public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext context;

    public TodoListDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }
    public IQueryable<TodoList> GetTodoLists => this.context.TodoLists;

    public void CreateTodoList(TodoList todoList)
    {
        _ = this.context.TodoLists.Add(todoList);
        _ = this.context.SaveChanges();
    }

    public void DeleteTodoList(TodoList? todoList)
    {
        if (todoList != null)
        {
            _ = this.context.TodoLists.Remove(todoList);
            _ = this.context.SaveChanges();
        }
    }

    public TodoList? GetTodoList(long id)
    {
        var todoListEntity = this.context.TodoLists.FirstOrDefault(x => x.Id == id);
        if (todoListEntity == null)
        {
            return null;
        }

        return todoListEntity;
    }

    public void UpdateTodoList(TodoList todoList)
    {
        if (todoList.Id == 0)
        {
            _ = this.context.Add(todoList);
        }
        else
        {
            TodoList? dbEntry = this.context.TodoLists?.FirstOrDefault(td => td.Id == todoList.Id);

            if (dbEntry != null)
            {
                dbEntry.Title = todoList.Title;
                dbEntry.Description = todoList.Description;
                dbEntry.Tag = todoList.Tag;
                dbEntry.TaskStatus = todoList.TaskStatus;
            }
        }

        _ = this.context.SaveChanges();
    }
}
