using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WPF2.Models;

namespace WPF2
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyDbContext() : base("MyConnectionString") { }
    }
}
