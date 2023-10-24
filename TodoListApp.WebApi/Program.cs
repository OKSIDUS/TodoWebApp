using Microsoft.EntityFrameworkCore;
using TodoListApp.Services;
using TodoListApp.Services.Database;

namespace TodoListApp.WebApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        _ = builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen();

        _ = builder.Services.AddDbContext<TodoListDbContext>(opts =>
        {
            _ = opts.UseSqlServer(builder.Configuration["ConnectionStrings:TodoListConnection"]);
        });

        _ = builder.Services.AddScoped<ITodoListService, TodoListDatabaseService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
        }

        _ = app.UseHttpsRedirection();

        _ = app.UseAuthorization();
        _ = app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        _ = app.MapControllers();

        app.Run();
    }
}
