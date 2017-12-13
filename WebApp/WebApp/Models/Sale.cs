using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Sale
    {
       public string Id { get; set; }
        public User User { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public DateTime Date { get; set; }
    }
}