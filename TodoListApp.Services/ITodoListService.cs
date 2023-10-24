namespace TodoListApp.Services;
public interface ITodoListService
{
    public IQueryable<TodoList> GetTodoLists { get; }

    public TodoList? GetTodoList(long id);

    public void CreateTodoList(TodoList todoList);

    public void UpdateTodoList(TodoList todoList);

    public void DeleteTodoList(TodoList todoList);
}
