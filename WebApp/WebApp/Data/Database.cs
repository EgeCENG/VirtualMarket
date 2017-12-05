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
        private string path = "products.txt";

        public List<Product> GetProduct()
        {
            string jsonText;
            if (!File.Exists(path))
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
            return null;
        }

        public void WriteProduct(List<Product> products)
        {
            
        }
        public bool Add(Product product)
        {
            List<Product> products = GetProduct();
            products.Add(product);
            return true;
        }
        public bool Delete(Product product)
        {
            return true;
        }
        public bool Delete(int id)
        {
            return true;
        }
    }
}