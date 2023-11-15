using AutoMapper;
using TodoListApp.Services.Database.Entity;
using TodoListApp.Services.interfaces;

namespace TodoListApp.Services.Database.Services;
public class UserDatabaseService : IUserService
{
    private readonly TodoListDbContext context;
    private readonly IMapper mapper;

    public UserDatabaseService(TodoListDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void CreateUser(User user)
    {
        this.context.Users.Add(new UserEntity { Name = user.Name, Password = user.Password });

        this.context.SaveChanges();
    }

    public User GetUser(int userId)
    {
        var userEntity = this.context.Users.Where(u => u.Id == userId).FirstOrDefault();
        if (userEntity is null)
        {
            throw new ArgumentNullException(nameof(userId));
        }

        return this.mapper.Map<User>(userEntity);
    }

    public IEnumerable<User> GetUsers()
    {
        var users = this.context.Users.ToList();
        return users.Select(u => this.mapper.Map<User>(u));
    }

    public IEnumerable<Task> GetUserTasks(int userId)
    {
        var tasks = this.context.Tasks.Where(t => t.UserId == userId).ToList();
        return tasks.Select(t => this.mapper.Map<Task>(t));
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
        this.mapper.Map(user, userEntity);

        this.context.Users.Update(userEntity);

        this.context.SaveChanges();
    }
}
