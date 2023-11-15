namespace TodoListApp.Services.interfaces;
public interface ITodoListService
{
    public IEnumerable<TodoList> GetTodoLists();

    public TodoList? GetTodoList(int id);

    public void RemoveTodoList(int id);

    public void UpdateTodoList(TodoList todoList);

    public void CreateTodoList(TodoList todoList);
}
