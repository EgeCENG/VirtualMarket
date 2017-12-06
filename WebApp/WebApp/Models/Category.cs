using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testPA3
{
    class Category
    {
        //Kategorilerin tutulacağı HashTable
        Hashtable category = new Hashtable();        
        //Category constructors
        public Category() { }
        public Category(string category_name, BSProductNameTree productsOfCategory)
        {
            category.Add(category_name, productsOfCategory);
        }
    }
}
