using Microsoft.EntityFrameworkCore;
using TodoListApp.Common;
using TodoListApp.Services.Database;
using TodoListApp.Services.Database.Services;
using TodoListApp.Services.interfaces;

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

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddDbContext<TodoListDbContext>((services, options) =>
        {
            var configuration = services.GetRequiredService<IConfiguration>();
            options.UseSqlServer(configuration.GetConnectionString("TodoList"));
        });

        builder.Services.AddScoped<ITodoListService, TodoListDatabaseService>();
        builder.Services.AddScoped<ITaskService, TaskDatabaseService>();
        builder.Services.AddScoped<IUserService, UserDatabaseService>();
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
