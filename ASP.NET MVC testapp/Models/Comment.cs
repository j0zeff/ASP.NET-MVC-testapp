using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; } 
        public string CommentText { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DateAndTime { get; set; }

        public ApplicationUser User;
    }
}
