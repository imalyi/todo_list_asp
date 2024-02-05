using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ExpensesAppTracker.Models;
using ExpenseAppTracker.Models;
public class AccountController : Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [AllowAnonymous]
    public async Task<IActionResult> Login(AccountModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Username == "user" && model.Password == "user_password")
            {
                await AuthenticateUser("User");
                TempData["name"] = model.Username;
                return RedirectToAction("Index", "ExpenseItem");
            }
            else if (model.Username == "guest" && model.Password == "guest_password")
            {
                await AuthenticateUser("Guest");
                TempData["name"] = model.Username;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }

        return View(model);
    }

    private async Task AuthenticateUser(string role)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, role),
        new Claim(ClaimTypes.Role, role)
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            // Ustawienia właściwości autentykacji
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                      new ClaimsPrincipal(claimsIdentity),
                                      authProperties);
    }


    [Authorize]
    public IActionResult AdminAction()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    public IActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    public IActionResult Hello()
    {
        string name = TempData["name"] as string;
        ViewBag.name = name;
        return View();
    }
}