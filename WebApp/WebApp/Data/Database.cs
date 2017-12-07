using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;

namespace WebApp.Data
{
    public class Database
    {
        private string path = HttpContext.Current.Server.MapPath("~/bin") + "products.json";

        public List<Product> Deserialize()
        {
            string jsonText;
            if (File.Exists(path))
            {
                jsonText = File.ReadAllText(path);
                if (jsonText != "")
                {
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonText);
                    return products;
                }
                else
                {
                    Debug.WriteLine("The file is empty"); 
                }
            }
            else
            {
                Debug.WriteLine("The file is not exist");
            }
            return new List<Product>();
        }

        public void Serializer(List<Product> products)
        {
            string json = JsonConvert.SerializeObject(products);
            if (!File.Exists(path))
                File.Create(path).Close();  
            File.WriteAllText(path, json);

        }
        
        public Product Get(string id)
        {
            Product product = Deserialize().Find(x => x.Id == id);
            return product;
        }
        public bool Add(Product product)
        {
            
            List<Product> products = Deserialize();

            //Tüm productları ağaca ekler
            BSProductNameTree bsProductNameTree = new BSProductNameTree();
            foreach (Product item in products)
            {
                BSTProductNode node = new BSTProductNode(item);
                bsProductNameTree.InsertNode(node);
            }
            //Tüm productları ağaçtan çeker
            //TODO
            products.Add(product);
            Serializer(products);
            return true;
        }
        public bool Delete(Product product)
        {
            List<Product> products = Deserialize();
            products.Remove(products.Find(x => x.Id == product.Id));
            Serializer(products);
            return true;
        }
        public bool Delete(string id)
        {
            List<Product> products = Deserialize();
            products.Remove(products.Find(x => x.Id.Equals(id)));
            Serializer(products);
            return true;
        }
        public bool DeleteBy(Func<Product, bool> expression)
        {
            List<Product> products = Deserialize();
            var select = products.Where(expression);
            foreach (var item in select)
            {
                products.Remove(item);
            }
            Serializer(products);
            return true;
        }
        public void Update(Product product)
        {
            List<Product> products = Deserialize();
            Product dbProduct = products.Find(x => x.Id == product.Id);
            products.Remove(dbProduct);
            products.Add(product);
            Serializer(products);

        }

        public void SearchBy(Func<Product, bool> expression)
        {
            List<Product> products = Deserialize();
            products = (List<Product>) products.Where(expression);
        }
        public void DeleteFile()
        {
            if (File.Exists(path))
            File.Delete(path);
        }
        //Ağaç işleri

      
    }
}