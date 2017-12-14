using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public double Salery { get; set; }
        public bool Sex { get; set; }
        public int Age { get; set; }
        public int Role { get; set; } //0--> User ,  1--> Admin
    }
}