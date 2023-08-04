namespace ASP.NET_MVC_testapp.Models
{
    public class ListViewModel
    {
        public PaginatedList<Book> booklist { get; set; }
        public string name { get; set; }

    }
}
