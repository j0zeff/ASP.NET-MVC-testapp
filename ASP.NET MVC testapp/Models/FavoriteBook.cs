using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_MVC_testapp.Models
{
    public class FavoriteBook
    {
        [Key]
        public string user_id { get; set; }
        public int book_id { get; set; }
    }
}
