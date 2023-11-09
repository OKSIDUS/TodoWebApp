namespace TodoListApp.Services;
public interface ITodoListServiceAsync
{
    public Task<IEnumerable<TodoList>> GetTodoListsAsync();

    public Task<TodoList> GetTodoListAsync(int id);

    public Task RemoveTodoListAsync(int id);

    public Task UpdateTodoListAsync(TodoList todoList);

    public Task CreateTodoListAsync(TodoList todoList);
}
