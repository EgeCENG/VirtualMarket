using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    class DescDictionary
    {
        Hashtable descList = new Hashtable();
        //keyword e göre ürün referan tutulacak list
        List<Product> productList = new List<Product>();
        //constructors
        public DescDictionary() { }
        public DescDictionary(string keyword,Product descProduct)
        {
            productList.Add(descProduct);
            descList.Add(keyword, productList);
        }
       

    }
}
