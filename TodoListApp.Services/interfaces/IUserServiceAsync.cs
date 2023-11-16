namespace TodoListApp.Services.interfaces;
public interface IUserServiceAsync
{
    Task<User?> CheckUser(string name, string password);
}
