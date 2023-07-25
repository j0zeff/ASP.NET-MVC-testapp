using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_testapp.Controllers
{
    public class MyBooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
