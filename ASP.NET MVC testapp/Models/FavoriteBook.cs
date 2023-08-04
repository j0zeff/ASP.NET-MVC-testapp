using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_MVC_testapp.Models
{
    public class FavoriteBook
    {
        [Key]
        public int Id { get; set; } 
        public string user_id { get; set; }
        public int book_id { get; set; }
        public bool favorite { get; set; }
        public bool alreadyRead { get; set; }
        public bool currentlyReading { get; set; }
        public bool willRead { get; set; }  
    }
}
