namespace ASP.NET_MVC_testapp.Models
{
    public class IndexViewModel
    {
       public List<Book> favoriteBooks { get; set; }
       public List<Book> alreadyRead { get; set; }
       public List<Book> willRead { get; set; }
       public List<Book> currentlyReading { get; set; }

    }
}
