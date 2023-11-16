using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services.interfaces;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApp.Controllers;
public class AccountController : Controller
{
    private readonly IUserServiceAsync service;
    private readonly IMapper mapper;

    public AccountController(IUserServiceAsync service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string name, string password)
    {
        var user = await this.service.CheckUser(name, password);
        if (user != null)
        {
            var userModel = this.mapper.Map<UserModel>(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString(CultureInfo.InvariantCulture)),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                AllowRefresh = true,
            };

            Response.Cookies.Append("UserCookie", userModel.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);


            string userCookieValue = HttpContext.Request.Cookies["UserCookie"];

            return this.RedirectToAction("Index", "TodoList");
        }

        return this.View();
    }
}
