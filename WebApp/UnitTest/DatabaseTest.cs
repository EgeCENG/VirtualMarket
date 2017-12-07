using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp;
using WebApp.Models;
using WebApp.Data;

namespace UnitTest
{
    [TestClass]
    public class DatabaseTest
    {
        private Database db;
        private Random rnd;

        [TestInitialize]
        public void TestInitialize()
        {
            db = new Database();
            db.DeleteFile();
            rnd = new Random();
           
        }
        [TestMethod]
        public void TestAdd()
        {
            // arrange  
            Product product = new Product(Guid.NewGuid().ToString(), "TestProduct");
            // act  
            db.Add(product);
            Product dbProduct = db.Deserialize()[0];
            // assert  
            Assert.AreEqual(dbProduct.Id,product.Id);
            db.DeleteFile();
        }

        [TestMethod]
        public void TestDelete()
        {
            // arrange  
            Product product = new Product(Guid.NewGuid().ToString(), "TestProduct");
            // act  
            db.Add(product);
            Product dbProduct = db.Deserialize()[0];
            db.Delete(dbProduct);
            List<Product> products = db.Deserialize();
            // assert  
            Assert.AreEqual(products.Count ,0);
        }

        [TestMethod]
        public void TestGet()
        {
            // arrange  
            Product product = new Product(Guid.NewGuid().ToString(), "TestProduct");
            // act  
            db.Add(product);
            List<Product> products = db.Deserialize();
            db.DeleteFile();
            // assert  
            Assert.AreNotEqual(products.Count,0);
        }

    }
}
