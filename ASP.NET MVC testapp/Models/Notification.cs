using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        
        public string Massege { get; set; }
        public DateTime NotificationDateTime { get; set; }

    }
}
