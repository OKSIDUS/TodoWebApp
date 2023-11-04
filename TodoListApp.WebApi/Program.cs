using TodoListApp.Services.Database;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Services;
using TodoListApp.Services.Database.Services;

namespace TodoListApp.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        _ = builder.Services.AddDbContext<TodoListDbContext>((services, options) =>
        {
            var configuration = services.GetRequiredService<IConfiguration>();
            _ = options.UseSqlServer(configuration.GetConnectionString("TodoList"));
        });

        _ = builder.Services.AddScoped<ITodoListService, TodoListDatabaseService>();
        _ = builder.Services.AddScoped<ITaskService, TaskDatabaseService>();
        _ = builder.Services.AddScoped<IUserService, UserDatabaseService>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
