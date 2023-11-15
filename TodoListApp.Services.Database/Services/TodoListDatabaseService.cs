using AutoMapper;
using TodoListApp.Services.Database.Entity;
using TodoListApp.Services.interfaces;

namespace TodoListApp.Services.Database.Services;
public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext dbContext;
    private readonly IMapper mapper;

    public TodoListDatabaseService(TodoListDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public void CreateTodoList(TodoList? todoList)
    {
        if (todoList != null)
        {
            var entity = new TodoListEntity
            {
                Title = todoList.Title,
                Description = todoList.Description,
            };
            this.dbContext.Add(entity);
            this.dbContext.SaveChanges();
        }
    }

    public TodoList? GetTodoList(int id)
    {
        var todoList = this.dbContext.TodoLists.Where(td => td.Id == id).FirstOrDefault();

        if (todoList != null)
        {
            return this.mapper.Map<TodoList>(todoList);
        }

        return null;
    }

    public IEnumerable<TodoList> GetTodoLists()
    {
        var todoLists = this.dbContext.TodoLists.ToList();
        return todoLists.Select(t => this.mapper.Map<TodoList>(t));
    }

    public void RemoveTodoList(int id)
    {
        var todoListEntity = this.dbContext.TodoLists.Where(t => t.Id == id).FirstOrDefault();
        if (todoListEntity != null)
        {
            this.dbContext.TodoLists.Remove(todoListEntity);
            this.dbContext.SaveChanges();
        }
    }

    public void UpdateTodoList(TodoList todoList)
    {
        if (todoList is null)
        {
            throw new ArgumentNullException(nameof(todoList));
        }

        var todoListEntity = this.dbContext.TodoLists.Where(t => t.Id == todoList.Id).FirstOrDefault();
        if (todoListEntity != null)
        {
            this.mapper.Map(todoList, todoListEntity);

            this.dbContext.Update(todoListEntity);

            this.dbContext.SaveChanges();
        }
    }
}
