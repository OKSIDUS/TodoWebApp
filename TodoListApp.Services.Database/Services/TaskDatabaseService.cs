using AutoMapper;
using TodoListApp.Services.Database.Entity;
using TodoListApp.Services.interfaces;

namespace TodoListApp.Services.Database.Services;
public class TaskDatabaseService : ITaskService
{
    private readonly TodoListDbContext context;
    private readonly IMapper mapper;

    public TaskDatabaseService(TodoListDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void CreateTask(Task? task)
    {
        if (task != null)
        {
            this.context.Tasks.Add(new TaskEntity
            {
                Title = task.Title,
                TodoListId = task.TodoListId,
                Status = task.Status,
                SharedFor = task.SharedFor,
            });

            this.context.SaveChanges();
        }
    }

    public void DeleteTask(int id)
    {
        var taskEntity = this.context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        if (taskEntity != null)
        {
            this.context.Tasks.Remove(taskEntity);
            this.context.SaveChanges();
        }
    }

    public IEnumerable<Task> GetAllTasks()
    {
        var taskEnity = this.context.Tasks.ToList();
        return taskEnity.Select(t => this.mapper.Map<Task>(t));
    }

    public Task? GetTask(int id)
    {
        var task = this.context.Tasks.Where(t => t.Id == id).FirstOrDefault();
        if (task is null)
        {
            return null;
        }

        return this.mapper.Map<Task>(task);
    }

    public IEnumerable<Task> GetTasks(int todoListID)
    {
        var tasks = this.context.Tasks.Where(t => t.TodoListId == todoListID).ToList();
        return tasks.Select(t => this.mapper.Map<Task>(t));
    }

    public void ShareTask(int taskId, string sharedFor)
    {
        var task = this.GetTask(taskId);

        if (task != null)
        {
            task.SharedFor = sharedFor;
            this.context.Tasks.Update(this.mapper.Map<TaskEntity>(task));
            this.context.SaveChanges();
        }
    }

    public void UpdateTask(Task? task)
    {
        if (task != null)
        {
            var taskEntity = this.context.Tasks.Where(t => t.Id == task.Id).FirstOrDefault();

            if (taskEntity != null)
            {
                this.mapper.Map(task, taskEntity);

                this.context.Tasks.Update(taskEntity);

                this.context.SaveChanges();
            }
        }
    }
}
