using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
    public class EventViewModel
    {
        [Key]
        public int Id { get; set; }
        public IFormFile image_data { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Title { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Organizer_name { get; set; }
        public string Organizer_email { get; set; }

    }
}
