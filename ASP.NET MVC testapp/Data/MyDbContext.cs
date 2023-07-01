using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP.NET_MVC_testapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class MyDbContext : IdentityDbContext
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

}
