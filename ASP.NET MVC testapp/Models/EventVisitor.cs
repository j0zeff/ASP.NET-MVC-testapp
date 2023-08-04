using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
    public class EventVisitor
    {
        [Key]
        public int id { get; set; }
        public string User_id { get; set; }
        public int Event_id { get; set;}
    }
}
