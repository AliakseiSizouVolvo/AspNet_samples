using Microsoft.AspNetCore.Mvc;
using NetAcademy.UI.Models;

namespace NetAcademy.UI.Controllers;

public class UserController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LoginUser(LoginModel model)
    {
        return Ok(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string email, string password)
    {
        return Ok(new { Email = email, Password = password });
    }
}