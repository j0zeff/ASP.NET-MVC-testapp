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
    public class ProfileModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MyDbContext _context;
        public ApplicationUser _user;
        public IndexViewModel _userReadingList;
        public List<Event> _userEventList = new List<Event>();
        public ProfileModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MyDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _user = _context.AplicationUsers.Where(b => b.Id == currentUserId).FirstOrDefault();
            _userReadingList = GetLists();
            var eventVisitors = _context.eventVisitors.Where(b => b.User_id == currentUserId).ToList();
            foreach(var visitor in eventVisitors)
            {
                var temp = _context.Events.Where(b => b.Id == visitor.Event_id).FirstOrDefault();
                if(temp != null)
                    _userEventList.Add(temp);
            }
            return Page();
        }

        public IndexViewModel GetLists()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var books = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId).ToList();
            List<Book> ReadBooks = new List<Book>();
            List<Book> CurrentlyReadingBooks = new List<Book>();
            List<Book> FavoriteBooks = new List<Book>();
            List<Book> ToReadBooks = new List<Book>();
            foreach (var book in books)
            {
                var Book = _context.Books.Where(b => b.BookId == book.book_id).FirstOrDefault();
                if (book.alreadyRead == true)
                    ReadBooks.Add(Book);
                if (book.willRead)
                    ToReadBooks.Add(Book);
                if (book.currentlyReading == true)
                    CurrentlyReadingBooks.Add(Book);
                if (book.favorite == true)
                    FavoriteBooks.Add(Book);
            }
            var viewModel = new IndexViewModel
            {
                favoriteBooks = FavoriteBooks,
                willRead = ToReadBooks,
                currentlyReading = CurrentlyReadingBooks,
                alreadyRead = ReadBooks,
            };
            return viewModel;
        }
    }
}
