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
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Google.Protobuf.WellKnownTypes;
using Humanizer;
using NuGet.Protocol.Plugins;
using System.Drawing;

namespace ASP.NET_MVC_testapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult SearchUsers(string searchTerm)
        {
            if (searchTerm.IsNullOrEmpty())
            {
                var FullList = _context.AplicationUsers.ToList();
                return PartialView("_UserList", FullList);
            }
            var searchResults = _context.AplicationUsers
            .Where(b => b.Id.Contains(searchTerm) || b.Email.Contains(searchTerm) || b.Firstname.Contains(searchTerm) || b.Lastname.Contains(searchTerm))
            .ToList();

            ViewBag.IsAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (ViewBag.IsAjaxRequest)
            {
                // AJAX request, return partial view
                return PartialView("_UserList", searchResults);
            }
            else
            {
                return View("Users");
            }
        }

        public IActionResult Index()
        {
            List<Event> _Eventlist = _context.Events.ToList();
            return View(_Eventlist);
        }

        public IActionResult Users(int? pageNumber)
        {
            int pageSize = 5;
            var users = _context.AplicationUsers.ToList();


            var userList = PaginatedList<ApplicationUser>.Create(users, pageNumber ?? 1, pageSize);
            return View(userList);
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.AplicationUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AplicationUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.AplicationUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            var _user = _context.AplicationUsers.Find(id);
            _user = user;
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(user);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Users");
            }
            return View(_user);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.AplicationUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'MyDbContext.Book'  is null.");
            }
            var user = await _context.AplicationUsers.FindAsync(id);
            if (user != null)
            {
                _context.AplicationUsers.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Users");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private bool UserExists(string id)
        {
            return (_context.AplicationUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize]
        public IActionResult Friends()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var friends = _context.Friends.Where(b => b.CurrentUserId == currentUserId).ToList();
            var list = new List<ApplicationUser>();
            foreach (var user in friends)
            {
                list.Add(_context.AplicationUsers.Where(b => b.Id == user.FriendId).FirstOrDefault());
            }
            return View(list);
        }
        [Authorize]
        public IActionResult PartialSearchFriend(string Search)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _context.AplicationUsers.Where(b => b.Nickname.Contains(Search) || b.Firstname.Contains(Search) || b.Lastname.Contains(Search)).ToList();
            foreach (var item in list)
            {
                var friend = _context.Friends.Where(b => b.CurrentUserId == currentUserId && b.FriendId == item.Id).FirstOrDefault();
                if (friend != null)
                {
                    item.IsFriend = true;
                }
                else
                    item.IsFriend = false;
            }
            list.Remove(list.Where(b => b.Id == currentUserId).FirstOrDefault());
            return PartialView(list);
        }

        [HttpPost]
        [Authorize]
        public IActionResult SendFriendRequest(string RecipientId)
        {
            string currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Sender = _context.AplicationUsers.Where(b => b.Id == currentUser).FirstOrDefault();
            var duplicate = _context.Notifications.Where(b => b.RecipientId == RecipientId && b.SenderId == currentUser && b.Massege.Contains("sent you friend request")).FirstOrDefault();
            if (duplicate == null)
            {
                var friendRequest = new Notification()
                {
                    RecipientId = RecipientId,
                    SenderId = currentUser,
                    NotificationDateTime = DateTime.Now,
                    Massege = Sender.Nickname + " sent you friend request",
                };
                _context.Notifications.Add(friendRequest);
                _context.SaveChanges();
                return Json(new { massege = "Successfuly sended!" });
            }
            return Json(new { massege = "Already sended!" });
        }
        [Authorize]
        public IActionResult DeleteFriend(string FriendId)
        {
            string CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var friend1 = _context.Friends.Where(b => b.FriendId == FriendId && b.CurrentUserId == CurrentUserId).FirstOrDefault();
            var friend2 = _context.Friends.Where(b => b.FriendId == CurrentUserId && b.CurrentUserId == FriendId).FirstOrDefault();
            if (friend1 != null && friend2 != null)
            {
                _context.Friends.Remove(friend1);
                _context.Friends.Remove(friend2);
                _context.SaveChanges();
                return RedirectToAction("Friends");
            }
            return RedirectToAction("Friends");
        }

        [Authorize]
        public IActionResult NotificationNum()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var num = _context.Notifications.Where(b => b.RecipientId == currentUserId).ToList().Count();
            return Json(new { num });
        }
        [Authorize]
        public IActionResult MyNotifications()
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var _notifications = _context.Notifications.Where(b => b.RecipientId == currentUserId).ToArray();
            MessageViewModel[] arr = new MessageViewModel[_notifications.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                MessageViewModel messageViewModel = new MessageViewModel();
                messageViewModel.Sender = _context.AplicationUsers.Where(b => b.Id == _notifications[i].SenderId).FirstOrDefault();
                messageViewModel.Notification = _notifications[i];
                arr[i] = messageViewModel;
            }
            return Json(arr);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AnswerToRequest(bool Claim, string SenderId)
        {
            var Sender = _context.AplicationUsers.Where(b => b.Id == SenderId).FirstOrDefault();
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notification = _context.Notifications.Where(b => b.SenderId == Sender.Id && b.RecipientId == currentUserId && b.Massege.Contains("sent you friend request")).FirstOrDefault();
            Friend friend1 = new Friend()
            {
                CurrentUserId = currentUserId,
                FriendId = SenderId,
            };
            Friend friend2 = new Friend()
            {
                CurrentUserId = SenderId,
                FriendId = currentUserId,
            };
            if (Claim)
            {
                _context.Friends.Add(friend1);
                _context.Friends.Add(friend2);
                _context.Notifications.Remove(notification);
                _context.SaveChanges();
            }
            else
            {
                _context.Notifications.Remove(notification);
                _context.SaveChanges();
            }
            return Json(new { success = true });
        }

        public IActionResult UserProfile(string UserId)
        {
            var eventVisitors = _context.eventVisitors.Where(b => b.User_id == UserId).ToList();
            var _eventList = new List<Event>();
            foreach (var visitor in eventVisitors)
            {
                var temp = _context.Events.Where(b => b.Id == visitor.Event_id).FirstOrDefault();
                if (temp != null)
                    _eventList.Add(temp);
            }
            var _userProfile = new UserProfileViewModel()
            {
                _user = _context.AplicationUsers.Where(b => b.Id == UserId).FirstOrDefault(),
                _userEventList = _eventList,
                _userReadingList = GetUserLists(UserId),
            };
            return View(_userProfile);
        }
        public IndexViewModel GetUserLists(string UserId)
        {
            var books = _context.UserFavoriteBooks.Where(b => b.user_id == UserId).ToList();
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

        public IActionResult ShowUserBookList(string UserId, int num)
        {
            var list = new ListViewModel();
            var lists = GetUserLists(UserId);
            if(num == 1)
            {
                list.booklist = lists.alreadyRead;
                list.name = "already";
                list.userNickname = _context.AplicationUsers.Where(b => b.Id == UserId).Select(b => b.Nickname).FirstOrDefault();
            }
            else if (num == 2)
            {
                list.booklist = lists.currentlyReading;
                list.name = "currently";
                list.userNickname = _context.AplicationUsers.Where(b => b.Id == UserId).Select(b => b.Nickname).FirstOrDefault();
            }
            else if(num == 3) 
            {
                list.booklist = lists.willRead;
                list.name = "will";
                list.userNickname = _context.AplicationUsers.Where(b => b.Id == UserId).Select(b => b.Nickname).FirstOrDefault();
            }
            else
            {
                list.booklist= lists.favoriteBooks;
                list.name = "favorite";
                list.userNickname = _context.AplicationUsers.Where(b => b.Id == UserId).Select(b => b.Nickname).FirstOrDefault();
            }


            return View(list);
        }
    }
}