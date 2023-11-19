namespace TodoListApp.Services.interfaces;
public interface ITaskServiceAsync
{
    public Task<IEnumerable<Task>> GetTasksAsync(int todoListID);

    public Task<bool> CreateAsync(Task task);

    public Task<Task> GetTaskAsync(int id);

    public Task<bool> DeleteAsync(int id);

    public Task<bool> UpdateAsync(Task task);

    public Task<IEnumerable<Task>> AssignedTasks(string sharedFor);
}
