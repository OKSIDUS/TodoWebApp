namespace TodoListApp.Services.interfaces;
public interface ITodoListServiceAsync
{
    public Task<IEnumerable<TodoList>> GetTodoListsAsync(string createdBy);

    public Task<TodoList?> GetTodoListAsync(int id);

    public Task<bool> RemoveTodoListAsync(int id);

    public Task<bool> UpdateTodoListAsync(TodoList todoList);

    public Task<bool> CreateTodoListAsync(TodoList todoList);
}
