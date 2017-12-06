using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testPA3
{
    class Product
    {
        //Ürüne ait özellikler
        public string ProductCategory { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductModel { get; set; }
        public int ProductCount { get; set; }
        public double ProductCost { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDesc { get; set; }
        //Ürün constructors
        public Product() { }
        public Product(string pCategory, string pName, string pBrand, string pModel, int pCount, double pCost, double pPrice, string pDesc)
        {
            this.ProductCategory = pCategory;
            this.ProductName = pName;
            this.ProductBrand = pBrand;
            this.ProductModel = pModel;
            this.ProductCount = pCount;
            this.ProductCost = pCost;
            this.ProductPrice = pPrice;
            this.ProductDesc = pDesc;
        }
    }
}
