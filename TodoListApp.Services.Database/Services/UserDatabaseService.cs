using Microsoft.EntityFrameworkCore;
using TodoListApp.Services.Database.Entity;

namespace TodoListApp.Services.Database.Services;
public class UserDatabaseService : IUserService
{
    private readonly TodoListDbContext context;

    public UserDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }

    public void CreateUser(User user)
    {
        _ = this.context.Users.Add(new UserEntity
        {
            //Id = user.Id,
            Name = user.Name,
            Password = user.Password,
        });

        _ = this.context.SaveChanges();
    }

    public User GetUser(int userId)
    {
        var taskEntity = this.context.Users.Where(u => u.Id == userId).FirstOrDefault();
        if (taskEntity is null)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        return new User
        {
            Id = taskEntity.Id,
            Name = taskEntity.Name,
            Password = taskEntity.Password,
        };
    }

    public IEnumerable<User> GetUsers()
    {
        var users = this.context.Users.ToList();
        return users.Select(u => new User
        {
            Id = u.Id,
            Name = u.Name,
            Password = u.Password,
        });
    }

    public IEnumerable<Task> GetUserTasks(int userId)
    {
        var tasks = this.context.Tasks.Where(t => t.UserId == userId).ToList();
        return tasks.Select(t => new Task
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status,
            TodoListId = t.TodoListId,
            UserId = t.UserId,
        });
    }

    public void UpdateUser(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (user.Id == 0)
        {
            this.CreateUser(user);
        }

        var userEntity = this.context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
        userEntity.Name = user.Name;
        userEntity.Password = user.Password;

        _ = this.context.Users.Update(userEntity);

        _ = this.context.SaveChanges();
    }
}
