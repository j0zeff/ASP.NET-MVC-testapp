using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASP.NET_MVC_testapp.Models;
using ASP.NET_MVC_testapp.Controllers;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace ASP.NET_MVC_testapp.Areas.Identity.Pages.Account.Manage
{
    public class EditProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyDbContext _context;
        public ApplicationUser _user;
        public string _firstName;
        public string _lastName;
        public string _nickname;
        public string _userDescription;
        public string _userGoals;
        public IFormFile _userImage;

        public EditProfileModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MyDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _user = _context.AplicationUsers.Where(b => b.Id == currentUser).FirstOrDefault();
            _firstName = _user.Firstname;
            _lastName = _user.Lastname;
            _nickname = _user.Nickname;
            _userDescription = _user.UserDescription;
            _userGoals = _user.UserGoals;
            return Page();
        }
        public IActionResult OnPostEditProfile(string _firstName, string _lastName, string _nickname, string _userDescription, string _userGoals, IFormFile _userImage)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _user = _context.AplicationUsers.Where(b => b.Id == currentUser).FirstOrDefault();
            if (!_firstName.IsNullOrEmpty())
                _user.Firstname = _firstName;
            if (!_lastName.IsNullOrEmpty())
                _user.Lastname = _lastName;
            if (!_nickname.IsNullOrEmpty())
                _user.Nickname = _nickname;
            if (!_userDescription.IsNullOrEmpty())
                _user.UserDescription = _userDescription;
            if (!_userGoals.IsNullOrEmpty())
                _user.UserGoals = _userGoals;
            if (_userImage != null && _userImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    _userImage.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    _user.UserImage = imageBytes;
                }
            }
            _context.AplicationUsers.Update(_user);
            _context.SaveChanges();
            return RedirectToPage("Profile");
        }
    }
}
