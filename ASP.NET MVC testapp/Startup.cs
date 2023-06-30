using ASP.NET_MVC_testapp.Repositoty;
using Microsoft.EntityFrameworkCore;

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
