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
using System.Security.Claims;
using System.Data.Entity;
using System.Drawing.Printing;
using Microsoft.VisualBasic;


namespace ASP.NET_MVC_testapp.Controllers
{
    public class MyBooksController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger _logger;

        public MyBooksController(MyDbContext context, ILogger<MyBooksController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void AddToFavorites(int bookId, bool IsFavorite)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var temp = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.book_id == bookId).ToList().FirstOrDefault();
            if (temp != null)
            {
                temp.favorite = IsFavorite;
                _context.SaveChanges();
            }
            else if (bookId != null && currentUserId != null)
            {
                    var favoriteBook = new FavoriteBook { book_id = bookId, user_id = currentUserId, favorite = IsFavorite };
                    _context.UserFavoriteBooks.Add(favoriteBook);
                    _context.SaveChanges();
            }
            //return RedirectToRoute(new { action = "IndexLib", controller = "Books" });
        }
    

    public IActionResult AddToAlreadyRead(int bookId, bool IsAlreadyRead)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var temp = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.book_id == bookId).ToList().FirstOrDefault();
            if (temp != null)
            {
                temp.alreadyRead = IsAlreadyRead;
                _context.SaveChanges();
            }
            else if (bookId != null && currentUserId != null)
            {
                    var favoriteBook = new FavoriteBook { book_id = bookId, user_id = currentUserId, alreadyRead = IsAlreadyRead };
                    _context.UserFavoriteBooks.Add(favoriteBook);
                    _context.SaveChanges();
            }
            return RedirectToRoute(new { action = "IndexLib", controller = "Books" });
        }

        public IActionResult AddToCurrentlyReading(int bookId, bool IsCurrentlyReading)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var temp = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.book_id == bookId).ToList().FirstOrDefault();
            if (temp != null)
            {
                temp.currentlyReading = IsCurrentlyReading;
                _context.SaveChanges();
            }
            else if (bookId != null && currentUserId != null)
            {
                var favoriteBook = new FavoriteBook { book_id = bookId, user_id = currentUserId, currentlyReading = IsCurrentlyReading };
                _context.UserFavoriteBooks.Add(favoriteBook);
                _context.SaveChanges();
            }
            return RedirectToRoute(new { action = "IndexLib", controller = "Books" });
        }

        public IActionResult AddToWillRead(int bookId, bool IsWillRead)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var temp = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.book_id == bookId).ToList().FirstOrDefault();
            if (temp != null)
            {
                temp.willRead = IsWillRead;
                _context.SaveChanges();
            }
            else if (bookId != null && currentUserId != null)
            {
                var favoriteBook = new FavoriteBook { book_id = bookId, user_id = currentUserId, willRead = IsWillRead };
                _context.UserFavoriteBooks.Add(favoriteBook);
                _context.SaveChanges();
            }
            return RedirectToRoute(new { action = "IndexLib", controller = "Books" });
        }
        public IActionResult Index() {
            var viewModel = GetLists();
            return View(viewModel);
        }
        public IActionResult ShowList(int num, int? pageNumber)
        {
            var ViewModel = GetLists();
            int pageSize = 5;
            if (num == 1)
                return View(PaginatedList<Book>.Create(ViewModel.alreadyRead, pageNumber ?? 1, pageSize));
            else if(num == 2)
                return View(PaginatedList<Book>.Create(ViewModel.currentlyReading, pageNumber ?? 1, pageSize));
            else if(num == 3)
                return View(PaginatedList<Book>.Create(ViewModel.willRead, pageNumber ?? 1, pageSize));
            else if(num ==4 )
                return View(PaginatedList<Book>.Create(ViewModel.favoriteBooks, pageNumber ?? 1, pageSize));
            return NotFound();
        }
        public IActionResult PaginatedList(int? pageNumber, int num) 
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int pageSize = 5;
            if (num == 1)
            {
                var favoriteBooks = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.alreadyRead == true).ToList();
                List<Book> listbook = new List<Book>();
                foreach (var item in favoriteBooks)
                {
                    listbook.Add(_context.Books.Where(b => b.BookId == item.book_id).FirstOrDefault());
                }

                return View("ShowList", PaginatedList<Book>.Create(listbook, pageNumber ?? 1, pageSize));
            }
            else if (num == 2)
            {
                var favoriteBooks = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.currentlyReading == true).ToList();
                List<Book> listbook = new List<Book>();
                foreach (var item in favoriteBooks)
                {
                    listbook.Add(_context.Books.Where(b => b.BookId == item.book_id).FirstOrDefault());
                }

                return View("ShowList", PaginatedList<Book>.Create(listbook, pageNumber ?? 1, pageSize));
            }
            else if (num == 3)
            {
                var favoriteBooks = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.willRead == true).ToList();
                List<Book> listbook = new List<Book>();
                foreach (var item in favoriteBooks)
                {
                    listbook.Add(_context.Books.Where(b => b.BookId == item.book_id).FirstOrDefault());
                }

                return View("ShowList", PaginatedList<Book>.Create(listbook, pageNumber ?? 1, pageSize));
            }
            else
            {
                var favoriteBooks = _context.UserFavoriteBooks.Where(b => b.user_id == currentUserId && b.favorite == true).ToList();
                List<Book> listbook = new List<Book>();
                foreach (var item in favoriteBooks)
                {
                    listbook.Add(_context.Books.Where(b => b.BookId == item.book_id).FirstOrDefault());
                }

                return View("ShowList", PaginatedList<Book>.Create(listbook, pageNumber ?? 1, pageSize));
            }
        }
        public IndexViewModel GetLists() {
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
        public IActionResult SearchBooks(string searchTerm, List<Book> books)
        {
            if (searchTerm.IsNullOrEmpty())
            {
                return PartialView("_BookList", books);
            }
            var searchResults = books
            .Where(b => b.Title.Contains(searchTerm) || b.AuthorName.Contains(searchTerm) || b.AuthorSurname.Contains(searchTerm) || b.ReleaseDate.ToString().Contains(searchTerm))
            .ToList();

            ViewBag.IsAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (ViewBag.IsAjaxRequest)
            {
                return PartialView("_BookList", searchResults);
            }
            else
            {
                return View("IndexLib");
            }
        }
        [HttpPost]
        public IActionResult OnPostAddToMyBook(int bookId, string selectedOption)
        {
            switch (selectedOption)
            {
                case "favorites":
                    AddToFavorites(bookId, true);
                    break;
                case "alreadyRead":
                    AddToAlreadyRead(bookId, true);
                    break;
                case "currentlyReading":
                    AddToCurrentlyReading(bookId, true);
                    break;
                case "willRead":
                    AddToWillRead(bookId, true);
                    break;
                default:
                    
                    break;
            }
            return RedirectToRoute(new { action = "IndexLib", controller = "Books" });
        }
    }
}


