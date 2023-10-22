namespace TodoListApp.Services;
public interface ITodoListService
{
    public IQueryable<TodoList> GetTodoLists { get; }

    public void CreateTodoList(TodoList todoList);

    public void SaveTodoList(TodoList todoList);

    public void DeleteTodoList(TodoList todoList);
}
