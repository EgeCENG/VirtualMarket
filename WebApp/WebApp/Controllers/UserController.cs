using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(List<Product> products )
        {
            return View();
        }

        public ActionResult Search(string search,double max,double min)
        {
            if (max > 0)
            {
                if (search != null)
                {
                    //TODO Search by Name , Price
                }
                else
                {
                    //TODO Search by Price
                }
            }
            else
            {
                if (search != null)
                {
                    //TODO Search by Name
                }
            }
            // Mock data
            List<Product> products = new List<Product>
            {
                new Product
                {
                    Brand = "test",
                    Category = "test",
                    Cost = 5000,
                    Count = 20,
                    Desc = "asdasdsa",
                    Id = "TestID",
                    Model = "modeltest",
                    Name = "nameTest",
                    Price = 6000
                },
                new Product
                {
                    Brand = "test",
                    Category = "test",
                    Cost = 5000,
                    Count = 20,
                    Desc = "asdasdsa",
                    Id = "TestID2",
                    Model = "modeltest",
                    Name = "nameTest",
                    Price = 6000
                },
                new Product
                {
                    Brand = "test",
                    Category = "test",
                    Cost = 5000,
                    Count = 20,
                    Desc = "asdasdsa",
                    Id = "TestID3",
                    Model = "modeltest",
                    Name = "nameTest",
                    Price = 6000
                }
            };

            return RedirectToAction("Index",products);
        }

        public void AddToShoppingCart(Product product)
        {
            List<Product> shoppingCart = Session["Cart"] as List<Product>;
            shoppingCart.Add(product);
            Session["Cart"] = shoppingCart;
        }

        public ActionResult Checkout()
        {
            return View();
        }

    }
}