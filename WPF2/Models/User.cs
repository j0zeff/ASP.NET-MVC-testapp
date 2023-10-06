using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF2.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string Email { get; set; }
    }
}
