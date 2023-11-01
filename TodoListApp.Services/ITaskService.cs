namespace TodoListApp.Services;
public interface ITaskService
{
    public IEnumerable<Task> GetTasks(int todoListID);

    public Task GetTask(int id);

    public void CreateTask(Task task);

    public void DeleteTask(int id);

    public void UpdateTask(Task task);
}
