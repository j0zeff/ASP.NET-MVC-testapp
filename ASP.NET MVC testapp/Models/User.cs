using System.ComponentModel.DataAnnotations;

namespace ASP.NET_MVC_testapp.Models
{
	public class User
	{
		[Key]
		public int IdUser { get; set; }
		public string UserName { get; set; }
		public string UserSurname { get; set; }
		public string Nickname { get; set; }
		public int Age { get; set; }
		public string Email { get; set; }
		public string UserDescription { get; set; }
		public string UserMod { get; set; }


	}
}
