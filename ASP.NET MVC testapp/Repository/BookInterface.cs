using ASP.NET_MVC_testapp.Models;

namespace ASP.NET_MVC_testapp
{
    public interface BookInterface
    {
        ICollection<Book> Books { get; }
    }
}
