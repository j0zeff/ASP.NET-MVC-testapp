using ASP.NET_MVC_testapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_testapp.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult SignUp()
        {
            var users = new ApplicationUser();
            return View(users);
        }
        public IActionResult LogIn()
        {
            var users = new ApplicationUser();
            return View(users);
        }
    }
}
