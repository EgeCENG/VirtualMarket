using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private ProductRepository _productRepository;
        private UserRepository _userRepository;
        private ShoppingCartRepository _shoppingCartRepository;
        private SaleRepository _saleRepository;

        public UserController()
        {
            _productRepository = new ProductRepository();
            _userRepository = new UserRepository();
            _shoppingCartRepository = new ShoppingCartRepository();
            _saleRepository = new SaleRepository();
        }

        public ActionResult Index()
        {
            return View(_productRepository.GetAllProduct());
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

        
        public ActionResult AddToShoppingCart(string productId)
        {
            Product product = _productRepository.Get(productId);
            ShoppingCart shoppingCart = Session["Cart"] as ShoppingCart;
            if (shoppingCart != null)
            {
                shoppingCart.ProductList.Add(product);
            }
            else
            {
                shoppingCart = new ShoppingCart();
                shoppingCart.ProductList = new List<Product>();
                product.Count = 1;
                shoppingCart.ProductList.Add(product);
            }
            Session["Cart"] = shoppingCart;
            return RedirectToAction("Index","User");
        }

        public void SortByPrice(string category)
        {
            HeapProduct heapProduct = new HeapProduct(category);
          //  BSProductNameTree bstNameTree = productRepository.CategoryHash[category];
           // heapProduct.Insert();
        }
        public ActionResult ShoppingCart()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;

            return View(cart);
        }

        public ActionResult Checkout()
        {
            User user = Session["User"] as User;
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            _shoppingCartRepository.Add(cart);
            _saleRepository.Add(new Sale{Id=Guid.NewGuid().ToString(),ShoppingCart = cart,User = user});
            //TODO stoktan ürün düşülecek
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            _userRepository.Add(user);
            Session["User"] = user;
            return RedirectToAction("Index","Home");
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            User userRepo = _userRepository.Login(user);
            if (userRepo != null)
            {
                Session["User"] = userRepo;
                return RedirectToAction("Index","Product");
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public ActionResult Tree()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tree(string category)
        {

            return View();
        }

        public ActionResult Heap()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Heap(string category)
        {
            HeapProduct heapProduct = new HeapProduct(category);
            return View(heapProduct.heapProductList);
        }

    }
}