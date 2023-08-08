using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
    public class BookViewModel
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }
        public string BookDescription { get; set; }
        public int ReleaseDate { get; set; }
        public IFormFile Book_image { get; set; }
    }
}
