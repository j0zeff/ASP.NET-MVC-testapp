using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC_testapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ASP.NET_MVC_testapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        
        public AccountController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Profile()
        {
            return View();
        }

    }
}
