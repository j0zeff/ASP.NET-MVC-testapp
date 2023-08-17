using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_MVC_testapp.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public byte[] image_data { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Title { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Organizer_name { get; set; }
        public string Organizer_email { get; set; }
        public int visitors = 0;
        public bool isVisited;

    }
}
