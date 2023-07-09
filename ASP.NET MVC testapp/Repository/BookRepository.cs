using ASP.NET_MVC_testapp.Models;
using System.Linq;

namespace ASP.NET_MVC_testapp.Repository
{
    public class BookRepository : BookInterface
    {
        private readonly MyDbContext _myDbContext;

        public BookRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public List<Book> SearchBooks(string searchTerm)
        {
            return _myDbContext.Books.Where(b => b.Title.Contains(searchTerm) || b.AuthorName.Contains(searchTerm)).ToList();
        }
    }
}
