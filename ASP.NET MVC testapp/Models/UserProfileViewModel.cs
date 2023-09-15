namespace ASP.NET_MVC_testapp.Models
{
    public class UserProfileViewModel
    {
        public ApplicationUser _user;
        public IndexViewModel _userReadingList;
        public List<Event> _userEventList = new List<Event>();
    }
}
