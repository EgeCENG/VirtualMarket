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
        private string path;

        public JsonAdapter(string path)
        {
            this.path = path;
        }
        public List<T> Deserialize<T>()
        {
          
            string jsonText;
            if (File.Exists(path))
            {
                jsonText = File.ReadAllText(path);
                if (jsonText != "")
                {
                    List<T> deserializeObjects = JsonConvert.DeserializeObject<List<T>>(jsonText);
                    return deserializeObjects;
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

        public void Serialize<T>(List<T> serializeObjects)
        {
            string json = JsonConvert.SerializeObject(serializeObjects);
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