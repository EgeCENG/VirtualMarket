using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public class Product 
    {
        //Ürüne ait özellikler
        [Key]
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public string Desc { get; set; }
        //Ürün constructors
        public Product() { }
        public Product(string pCategory, string pName, string pBrand, string pModel, int pCount, double pCost, double pPrice, string pDesc)
        {
            this.Category = pCategory;
            this.Name = pName;
            this.Brand = pBrand;
            this.Model = pModel;
            this.Count = pCount;
            this.Cost = pCost;
            this.Price = pPrice;
            this.Desc = pDesc;
        }

        public Product(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
