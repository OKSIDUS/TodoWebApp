using TodoListApp.Services.Database.Entity;

namespace TodoListApp.Services.Database.Services;
public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext dbContext;

    public TodoListDatabaseService(TodoListDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void CreateTodoList(TodoList todoList)
    {
        var entity = new TodoListEntity
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Description = todoList.Description,
        };
        _ = this.dbContext.Add(entity);
        _ = this.dbContext.SaveChanges();
    }

    public TodoList GetTodoList(int id)
    {
        var todoList = this.dbContext.TodoLists.Where(td => td.Id == id).FirstOrDefault();

        if (todoList != null)
        {
            return new TodoList
            {
                Id = todoList.Id,
                Title = todoList.Title,
                Description = todoList.Description,
            };
        }

        return new TodoList
        {
            Id = id,
            Title = string.Empty,
            Description = string.Empty,
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
        });
    }

    public void RemoveTodoList(int id)
    {
        var todoListEntity = this.dbContext.TodoLists.Where(t => t.Id == id).FirstOrDefault();
        var tasks = this.dbContext.Tasks.Where(t => t.TodoListId == id).ToList();
        if (todoListEntity != null)
        {
            foreach (var task in tasks)
            {
                _ = this.dbContext.Tasks.Remove(task);
                _ = this.dbContext.SaveChanges();
            }

            _ = this.dbContext.TodoLists.Remove(todoListEntity);
            _ = this.dbContext.SaveChanges();
        }
    }

    public void UpdateTodoList(TodoList todoList)
    {
        if (todoList is null)
        {
            throw new ArgumentNullException(nameof(todoList));
        }

        if (todoList.Id == 0)
        {
            this.CreateTodoList(todoList);
        }
        else
        {
            var todoListEntity = this.dbContext.TodoLists.Where(t => t.Id == todoList.Id).FirstOrDefault();
            todoListEntity.Title = todoList.Title;
            todoListEntity.Description = todoList.Description;

            _ = this.dbContext.Update(todoListEntity);

            _ = this.dbContext.SaveChanges();
        }
    }
}
