using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_testapp.Controllers
{
    public class MyBooksController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
