using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_MVC_testapp.Models
{
    public class Book
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
        public byte[] Book_image { get; set; }
        public List<Genre> Genre_list;
    }
}
