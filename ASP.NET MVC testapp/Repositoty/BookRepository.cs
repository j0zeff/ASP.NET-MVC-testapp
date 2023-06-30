using ASP.NET_MVC_testapp.Models;
using System.Linq;

namespace ASP.NET_MVC_testapp.Repositoty
{
    public class BookRepository : BookInterface
    
    {
        private readonly MyDbContext myDbContext;

        public BookRepository(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public ICollection<Book> Books => myDbContext.Books.ToList();
    }
}
