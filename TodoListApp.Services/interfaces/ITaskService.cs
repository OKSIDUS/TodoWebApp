namespace TodoListApp.Services.interfaces;
public interface ITaskService
{
    public IEnumerable<Task> GetTasks(int todoListID);

    public IEnumerable<Task> GetAllTasks();

    public Task GetTask(int id);

    public void CreateTask(Task task);

    public void DeleteTask(int id);

    public void UpdateTask(Task task);

    public void ShareTask(int taskId, int userId);
}
