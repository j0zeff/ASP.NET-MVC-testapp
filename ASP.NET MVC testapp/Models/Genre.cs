using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
    public class Genre
    {
        [Key]
        public int id { get; set; }
        public string Genre_name { get; set; }
    }
}
