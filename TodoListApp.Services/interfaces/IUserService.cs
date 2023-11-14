namespace TodoListApp.Services.interfaces;
public interface IUserService
{
    public IEnumerable<Task> GetUserTasks(int userId);

    public void CreateUser(User user);

    public User GetUser(int userId);

    public IEnumerable<User> GetUsers();

    public void UpdateUser(User user);

}
