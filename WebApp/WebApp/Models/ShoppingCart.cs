using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; }
        public List<Product> ProductList { get; set;}
        public User User { get; set; }

    }
}