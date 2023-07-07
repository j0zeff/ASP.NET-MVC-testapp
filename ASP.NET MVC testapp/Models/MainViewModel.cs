using ASP.NET_MVC_testapp.Repository;

namespace ASP.NET_MVC_testapp.Models
{
    public class MainViewModel
    {
        public BookRepository Bookrepository { get; set; }
        public BookFilters Bookfilters { get; set; }
        public List<Book> Booklist { get; set;}

        public MainViewModel(BookFilters bookFilters, BookRepository bookRepository, List<Book> books)
        {
            Bookfilters = bookFilters;
            Bookrepository = bookRepository;
            Booklist = books;  
        }
    }
}
