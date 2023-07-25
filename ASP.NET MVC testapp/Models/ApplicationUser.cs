using Microsoft.AspNetCore.Identity;

namespace ASP.NET_MVC_testapp.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string RoleId { get; set; }
		public string Role { get; set; }
	}
}
