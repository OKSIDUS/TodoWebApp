namespace TodoListApp.Services;
public interface ITaskServiceAsync
{
    public Task<IEnumerable<Task>> GetTasksAsync(int todoListID);
}
