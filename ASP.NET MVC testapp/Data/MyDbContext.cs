using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC_testapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class MyDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public MyDbContext(IConfiguration configuration) 
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MyDbContext"));
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<AplicationUser> AplicationUsers  { get; set; }

}
