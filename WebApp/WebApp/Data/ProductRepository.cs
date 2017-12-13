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
        public Hashtable CategoryHash = new Hashtable();
        public Hashtable DescWordsHash = new Hashtable();
        
        private string path = HttpContext.Current.Server.MapPath("~/Json/") + "products.json";

        private JsonAdapter _jsonAdapter;

        public ProductRepository()
        {
            _jsonAdapter = new JsonAdapter(path);
            FillCategorys();
            FillTree();
        }
        private void DescToHashtable(Product product)
        {
            string[] words = product.Desc.Split(' ');
            foreach (var word in words)
            {
                if (DescWordsHash.ContainsKey(word))
                {
                    List<Product> wordProducts = DescWordsHash[word] as List<Product>;
                    wordProducts.Add(product);
                }
                else
                {
                    DescWordsHash.Add(word, new List<Product> { product });
                }

            }
        }

        #region Category

        private void FillCategorys()
        {
            List<Product> products = _jsonAdapter.Deserialize<Product>();
            foreach (var product in products)
            {
                CategoryHash.Add(product.Category, new BSProductNameTree());
            }
        }

        public void AddCategory(string category)
        {
            CategoryHash.Add(category, new BSProductNameTree());
        }
               
        #endregion


        #region ProductCRUD_GET

        public Product Get(string id)
        {
            Product product = _jsonAdapter.Deserialize<Product>().Find(x => x.Id == id);
            return product;
        }
        public bool Add(Product product)
        {

            List<Product> products = _jsonAdapter.Deserialize<Product>();

            foreach (Product item in products)
            {
                var category = CategoryHash.Keys;
                foreach (var cat in category)
                {
                    if (cat == item.Category)
                    {
                        var categoryTree = CategoryHash[cat] as BSProductNameTree;
                        BSTProductNode node = new BSTProductNode(product);
                        categoryTree.InsertNode(node);
                    }
                }
            }
            products.Add(product);
            _jsonAdapter.Serialize(products);
            return true;
        }
        public bool Delete(string id)
        {
            //İlgili json veri setinin silinmesi için gerekli olan kısım
            List<Product> products = GetAllProduct();
            products.Remove(products.Find(x => x.Id.Equals(id)));
            _jsonAdapter.Serialize(products);
            //Oluşturduğumuz veri yapısından silinmesi için gerekli olan kısım

            return true;
        }
        public bool DeleteBy(Func<Product, bool> expression)
        {
            List<Product> products = _jsonAdapter.Deserialize<Product>();
            var select = products.Where(expression);
            foreach (var item in select)
            {
                products.Remove(item);
            }
            _jsonAdapter.Serialize(products);
            return true;
        }
        public void Update(Product product)
        {
            List<Product> products = _jsonAdapter.Deserialize<Product>();
            Product dbProduct = products.Find(x => x.Id == product.Id);
            products.Remove(dbProduct);
            products.Add(product);
            _jsonAdapter.Serialize(products);

        }
        public List<Product> SearchBy(Func<Product, bool> expression)
        {
            List<Product> products = _jsonAdapter.Deserialize<Product>();
            return (List<Product>)products.Where(expression);
        }
        public List<Product> GetAllProduct()
        {  
            List<Product> products = new List<Product>();
            foreach (var cat in CategoryHash.Keys)
            {
                var tree = CategoryHash[cat] as BSProductNameTree;
                TreeToList(tree.GetRoot(), products);
            }
            return products;
        }

        public List<Product> GetProductByWord(string word)
        {
            if(DescWordsHash.ContainsKey(word))
            return (List<Product>) DescWordsHash[word];
            return null;
        }
        #endregion

        

        #region TreeProcess
        private void FillTree()
        {
            List<Product> products = _jsonAdapter.Deserialize<Product>();
            var category = CategoryHash.Keys;
            foreach (Product item in products)
            {
                DescToHashtable(item);
                foreach (var cat in category)
                {
                    if (cat.Equals(item.Category))
                    {
                        BSProductNameTree categoryTree = CategoryHash[cat] as BSProductNameTree;
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


        #endregion

    }
}