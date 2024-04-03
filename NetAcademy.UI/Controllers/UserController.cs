using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NetAcademy.Services.Abstractions;
using NetAcademy.UI.Models;

namespace NetAcademy.UI.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly Guid _userRoleId = new Guid("D84FB779-C32F-49B0-BC99-2823DF5DE31E");
    const string UserRoleName = "User";
    public UserController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    // GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginUser(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _userService.CheckIsUserWithEmailExists(model.Email) &
                await _userService.CheckPassword(model.Email, model.Password))
            {
                await SignInUser(model.Email, UserRoleName);
                return RedirectToAction("Index", "Home");

            }
        }
        return Ok(model);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _userService.CheckIsUserWithEmailExists(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "User with that email already exists");
                return View(model);
            }

            //should be taken from DB

            await _userService.RegisterUser(model.Email, model.Password, _userRoleId);
            await SignInUser(model.Email, UserRoleName);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View(model);
        }
    }

    private async Task SignInUser(string email, string role)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role),
            new Claim("Age", "18+")
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    }
}