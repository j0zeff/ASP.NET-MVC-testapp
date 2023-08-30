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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize your custom user entity configuration if needed
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<ApplicationUser> AplicationUsers  { get; set; }
    public DbSet<FavoriteBook> UserFavoriteBooks { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventVisitor> eventVisitors { get; set; }
    public DbSet<Genre> Genre { get; set; }
}
