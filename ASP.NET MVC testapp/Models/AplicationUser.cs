using Microsoft.AspNetCore.Identity;

namespace ASP.NET_MVC_testapp.Models
{
	public class AplicationUser : IdentityUser
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
	}
}
