namespace ASP.NET_MVC_testapp.Models
{
    public class IndexLibViewModel
    {
        public List<IndexLibBook> Lists_book = new List<IndexLibBook>();
    }

    public class IndexLibBook
    {
        public List<Book> list { get; set; }
        public string Genre { get; set; }
    }
}
