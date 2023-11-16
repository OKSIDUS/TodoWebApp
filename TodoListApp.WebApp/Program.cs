using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using TodoListApp.Common;
using TodoListApp.Services.interfaces;
using TodoListApp.Services.WebApi;
using TodoListApp.WebApp.Controllers;

namespace TodoListApp.WebApp;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<ITodoListServiceAsync, TodoListWebApiService>();
        builder.Services.AddScoped<ITaskServiceAsync, TaskWebApiService>();
        builder.Services.AddScoped<IUserServiceAsync, UserWebApiService>();
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.Name = "UserCookie";
                options.LoginPath = "/Account/Login";
            });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}");

        app.MapControllerRoute(
            name: "CreateTask",
            pattern: "Task/CreateTask/{todoListId:int}",
            defaults: new { controller = "Task", action = "CreateTask" });
        app.Run();
    }
}
