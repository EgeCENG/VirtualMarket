using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApp.Data
{
    public class JsonAdapter
    {
        private string path = HttpContext.Current.Server.MapPath("~/Json/") + "products.json";

        public List<T> Deserialize<T>()
        {
            string jsonText;
            if (File.Exists(path))
            {
                jsonText = File.ReadAllText(path);
                if (jsonText != "")
                {
                    List<T> products = JsonConvert.DeserializeObject<List<T>>(jsonText);
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
            return new List<T>();
        }

        public void Serialize(List<Product> products)
        {
            string json = JsonConvert.SerializeObject(products);
            if (!File.Exists(path))
                File.Create(path).Close();
            File.WriteAllText(path, json);

        }
        public void DeleteFile()
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}