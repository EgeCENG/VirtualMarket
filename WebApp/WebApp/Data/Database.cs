using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Data
{
    public class Database
    {
        private string path = "products.json";

        public List<Product> Deserializer()
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
        public bool Add(Product product)
        {
            List<Product> products = Deserializer();
            products.Add(product);
            Serializer(products);
            return true;
        }
        public bool Delete(Product product)
        {
            List<Product> products = Deserializer();
            products.Remove(products.Find(x => x.Id == product.Id));
            Serializer(products);
            return true;
        }
        public bool Delete(int id)
        {
            List<Product> products = Deserializer();
            products.Remove(products.Find(x => x.Id == id));
            Serializer(products);
            return true;
        }
        public void DeleteFile()
        {
            if (File.Exists(path))
            File.Delete(path);
        }
    }
}