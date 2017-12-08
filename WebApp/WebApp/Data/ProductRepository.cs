using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;

namespace WebApp.Data
{
    public class ProductRepository
    {
        public Hashtable categoryHash = new Hashtable();

        private JsonAdapter jsonAdapter;

        public ProductRepository()
        {
            jsonAdapter = new JsonAdapter();
            categoryHash.Add("Bilgisayar",new BSProductNameTree());
            categoryHash.Add("Giyim", new BSProductNameTree());
        }
        public Product Get(string id)
        {
            Product product = jsonAdapter.Deserialize<Product>().Find(x => x.Id == id);
            return product;
        }
        public bool Add(Product product)
        {
            
            List<Product> products = jsonAdapter.Deserialize<Product>();

            foreach (Product item in products)
            {
                var category = categoryHash.Keys;
                foreach (var cat in category)
                {
                    if (cat == item.Category)
                    {
                        var categoryTree = categoryHash[cat] as BSProductNameTree;
                        BSTProductNode node = new BSTProductNode(product);
                        categoryTree.InsertNode(node);
                    }
                }
            }
            products.Add(product);
            jsonAdapter.Serialize(products);
            return true;
        }
        public bool Delete(Product product)
        {
            List<Product> products = jsonAdapter.Deserialize<Product>();
            products.Remove(products.Find(x => x.Id == product.Id));
            jsonAdapter.Serialize(products);
            return true;
        }
        public bool Delete(string id)
        {
            List<Product> products = jsonAdapter.Deserialize<Product>();
            products.Remove(products.Find(x => x.Id.Equals(id)));
            jsonAdapter.Serialize(products);
            return true;
        }
        public bool DeleteBy(Func<Product, bool> expression)
        {
            List<Product> products = jsonAdapter.Deserialize<Product>();
            var select = products.Where(expression);
            foreach (var item in select)
            {
                products.Remove(item);
            }
            jsonAdapter.Serialize(products);
            return true;
        }
        public void Update(Product product)
        {
            List<Product> products = jsonAdapter.Deserialize<Product>();
            Product dbProduct = products.Find(x => x.Id == product.Id);
            products.Remove(dbProduct);
            products.Add(product);
            jsonAdapter.Serialize(products);

        }

        public List<Product> SearchBy(Func<Product, bool> expression)
        {
            List<Product> products = jsonAdapter.Deserialize<Product>();
            return (List<Product>) products.Where(expression);
        }

        public List<Product> GetAllProduct()
        {
            FillTree();
            List<Product> products = new List<Product>();
            foreach (var cat in categoryHash.Keys)
            {
                var tree = categoryHash[cat] as BSProductNameTree;
                TreeToList(tree.GetRoot(),products);
            }
            return products;
        }    
        //Ağaç işleri
        private void FillTree()
        {
            List<Product> products =  jsonAdapter.Deserialize<Product>();
            var category = categoryHash.Keys;
            foreach (Product item in products)
            {
               
                foreach (var cat in category)
                {
                    if (cat.Equals(item.Category))
                    {
                        BSProductNameTree categoryTree = categoryHash[cat] as BSProductNameTree;
                        BSTProductNode node = new BSTProductNode(item);
                        node.productList.Add(item);
                        categoryTree.InsertNode(node);
                    }
                }
            }
        }
        private void TreeToList(BSTProductNode node, List<Product> products)
        {
            if (node == null)
            {
                return;
            }
            products.AddRange(node.productList);
            TreeToList(node.leftChild, products);
            TreeToList(node.rightChild, products);
        } 
    }
}