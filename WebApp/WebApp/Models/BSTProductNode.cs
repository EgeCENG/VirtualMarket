using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    class BSTProductNode
    {
        public string subCategory;
        public List<Product> productInfo = new List<Product>();
        public BSTProductNode leftChild;
        public BSTProductNode rightChild;

        public BSTProductNode(Product product)
        {
            subCategory = product.Name;
            productInfo.Add(product);
        }
        public void DisplayNode()
        {
          /* Console.WriteLine("Düğüm Grubu : {0} ", subCategory);
            foreach (var item in productInfo)
            {
                Console.WriteLine("Ürün Kategori : {0}|Ürün Adı : {1}|Marka : {2}|Model : {3}|Adet : {4}|Maliyet : {5}|Satış : {6}|Açıklama : {7}", item.Category, item.Name, item.Brand, item.Model, item.Count, item.Cost, item.Price, item.Desc);
            }*/
        }
    }
}
