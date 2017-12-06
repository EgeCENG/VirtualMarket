using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testPA3
{
    class BSTProductNode
    {
        public string productName;
        public List<Product> productInfo = new List<Product>();
        public BSTProductNode leftChild;
        public BSTProductNode rightChild;
        public void DisplayNode()
        {
          /* Console.WriteLine("Düğüm Grubu : {0} ", productName);
            foreach (var item in productInfo)
            {
                Console.WriteLine("Ürün Kategori : {0}|Ürün Adı : {1}|Marka : {2}|Model : {3}|Adet : {4}|Maliyet : {5}|Satış : {6}|Açıklama : {7}", item.ProductCategory, item.ProductName, item.ProductBrand, item.ProductModel, item.ProductCount, item.ProductCost, item.ProductPrice, item.ProductDesc);
            }*/
        }
    }
}
