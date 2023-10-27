namespace TodoListApp.Services;
public interface ITodoListService
{
    public IEnumerable<TodoList> GetTodoLists();

    public TodoList GetTask(int id);

    public void RemoveTask(int id);

    public TodoList UpdateTask(TodoList task);

    public void CreateTask(TodoList task);


}
