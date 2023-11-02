namespace TodoListApp.Services;
public interface IUserService
{
    public IEnumerable<Task> GetUserTasks(int userId);

    public void CreateUser(User user);

}
