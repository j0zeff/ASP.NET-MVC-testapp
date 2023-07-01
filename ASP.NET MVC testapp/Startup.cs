using ASP.NET_MVC_testapp.Repository;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Configuration;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_MVC_testapp
{
    public class Startup
    {
        private IConfiguration _confString;


        [Obsolete]
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment HostEnv)
        {
            _confString = new ConfigurationBuilder().SetBasePath(HostEnv.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(_confString.GetConnectionString("MyDbContext")));
        }
        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
