using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using University.Application.Models;
using University.Application.Services.Abstract;

namespace University.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICookieUserService _service;

        public AccountController(ICookieUserService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = HttpContext.User;

            if (currentUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserRequest userRequest)
        {
            bool isLoggedIn = await _service.Login(userRequest);

            if(isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Error"] = "Invalid username or password. Current user does not exist.";
            return View("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }
    }
}
