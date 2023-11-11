namespace TodoListApp.Services;
public interface ITodoListServiceAsync
{
    public Task<IEnumerable<TodoList>> GetTodoListsAsync();

    public Task<TodoList> GetTodoListAsync(int id);

    public Task<bool> RemoveTodoListAsync(int id);

    public Task<bool> UpdateTodoListAsync(TodoList todoList);

    public Task<bool> CreateTodoListAsync(TodoList todoList);
}
