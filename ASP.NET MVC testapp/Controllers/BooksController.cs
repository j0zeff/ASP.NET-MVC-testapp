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
using Newtonsoft.Json;

namespace ASP.NET_MVC_testapp.Controllers
{

    public class BooksController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger _logger;   

        public BooksController(MyDbContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult AddToFavorites(int bookId)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (bookId != null && currentUserId != null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                        // Enable IDENTITY_INSERT for the UserFavoriteBooks table
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT UserFavoriteBooks ON;");

                        var favoriteBook = new FavoriteBook { book_id = bookId, user_id = currentUserId };
                        _context.UserFavoriteBooks.Add(favoriteBook);
                        _context.SaveChanges();

                        // Disable IDENTITY_INSERT after the insert operation
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT UserFavoriteBooks OFF;");

                        transaction.Commit();

                    return RedirectToAction(nameof(IndexLib));
                }
            }

            return BadRequest("Invalid bookId or currentUserId.");
        }

        public IActionResult RemoveToFavorites(int bookId)
        {


            return RedirectToAction(nameof(IndexLib));
        }

    public IActionResult SearchBooks(string searchTerm)
        {
            if (searchTerm.IsNullOrEmpty())
            {       
                var FullList = _context.Books.ToList();
                return PartialView("_BookList", FullList);
            }
            var searchResults = _context.Books
            .Where(b => b.Title.Contains(searchTerm) || b.AuthorName.Contains(searchTerm) || b.AuthorSurname.Contains(searchTerm) || b.ReleaseDate.ToString().Contains(searchTerm))
            .ToList();

            ViewBag.IsAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (ViewBag.IsAjaxRequest)
            {
                // AJAX request, return partial view
                return PartialView("_BookList", searchResults);
            }
            else
            {
                return View("IndexLib");
            }
        }

        // GET: Books
        public IActionResult IndexLib()
        {
            var books = new IndexLibViewModel();
            books.adventures = _context.Books.Where(b => b.Genre == "adventures").ToList();
            books.history = _context.Books.Where(b => b.Genre == "history").ToList();
            books.poetry = _context.Books.Where(b => b.Genre == "poetry").ToList();
            books.psychology = _context.Books.Where(b => b.Genre == "psychology").ToList();
            books.science = _context.Books.Where(b => b.Genre == "science").ToList();
            books.fantasy = _context.Books.Where(b => b.Genre == "fantasy").ToList();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel _book)
        {
            var book = new Book();
            book.BookId = _book.BookId;
            book.Title = _book.Title;
            book.AuthorName = _book.AuthorName;
            book.AuthorSurname = _book.AuthorSurname;
            book.BookDescription = _book.BookDescription;
            book.ReleaseDate = _book.ReleaseDate;
            book.Genre = _book.Genre;
            book.Pages = _book.Pages;
            
                if (_book.Book_image != null && _book.Book_image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await _book.Book_image.CopyToAsync(memoryStream);
                        book.Book_image = memoryStream.ToArray();
                    }
                }
                _context.Add(book);
                _context.SaveChanges();
                return RedirectToAction("IndexLib");
        }

        // GET: Books/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            var _book = new BookViewModel();
            _book.AuthorSurname = book.AuthorSurname;
            _book.BookDescription = book.BookDescription;
            _book.AuthorName = book.AuthorName;
            _book.BookId = book.BookId;
            _book.Title = book.Title;
            _book.Genre = book.Genre;
            _book.Pages = book.Pages;
            _book.ReleaseDate = book.ReleaseDate;
            
            return View(_book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookViewModel _book)
        {
            var book = _context.Books.Where(b => b.BookId == _book.BookId).FirstOrDefault();
            book.AuthorName = _book.AuthorName;
            book.AuthorSurname = _book.AuthorSurname;
            book.BookDescription = _book.BookDescription;
            book.ReleaseDate = _book.ReleaseDate;
            book.Genre = _book.Genre;
            book.Title = _book.Title;
            book.Pages = _book.Pages;
            if(_book.Book_image!= null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await _book.Book_image.CopyToAsync(memoryStream);
                    book.Book_image = memoryStream.ToArray();
                }
            }
            
                    _context.Books.Update(book);
                    await _context.SaveChangesAsync();
               
                return RedirectToAction("IndexLib");
            
           // return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(id!=0)
            {
                foreach (var item in _context.UserFavoriteBooks.Where(b => b.book_id == id).ToList())
                {
                    _context.UserFavoriteBooks.Remove(item);
                }                
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("IndexLib");
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
        public IActionResult ShowGenreList(string title)
        {
            GenreList genreList = new GenreList();
            genreList.title = title;

            if(title=="Science")
            {
               genreList.books = _context.Books.Where(b => b.Genre == "science").ToList();
            }
            else if (title == "Adventures")
            {
                genreList.books = _context.Books.Where(b => b.Genre == "adventures").ToList();
            }
            else if (title == "Poetry")
            {
                genreList.books = _context.Books.Where(b => b.Genre == "poetry").ToList();

            }
            else if (title == "Fantasy")
            {
                genreList.books = _context.Books.Where(b => b.Genre == "fantasy").ToList();
            }
            else if (title == "History")
            {
                genreList.books = _context.Books.Where(b => b.Genre == "history").ToList();
            }
            else if (title == "Psychology")
            {
                genreList.books = _context.Books.Where(b => b.Genre == "psychology").ToList();
            }
            return View(genreList);
        }
    }
}
