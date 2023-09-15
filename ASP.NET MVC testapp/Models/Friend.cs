using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; } 
        public string CurrentUserId { get; set; }    
        public string FriendId { get; set; }
    }
}
