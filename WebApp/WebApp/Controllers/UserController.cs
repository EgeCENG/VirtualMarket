using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        [HttpPost]
        public ActionResult Index(string search, double max=0,double min=0)
        {
            List<Product> products = new List<Product>();
            if (max > 0)
            {
                if (search != "")
                {
                   products = _productRepository.SearchByPrice(max, min, search);
                }
                else
                {
                   products = _productRepository.SearchByPrice(max, min);
                }
            }
            else
            {
                if (search != null)
                {
                   products = _productRepository.SearchByName(search);
                }
            }

            return View(products);
        }

        
        public ActionResult AddToShoppingCart(string productId)
        {
            Product product = _productRepository.Get(productId);
            ShoppingCart shoppingCart = Session["Cart"] as ShoppingCart;
            if (shoppingCart != null)
            {
                product.Count = 1;
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
                return RedirectToAction("Index","Home");
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

        public ActionResult SuggestionSystems()
        {
            User user = Session["User"] as User;
            List<Sale> saleUsers =_saleRepository.GetAll();
            List<ViewModel> viewModel = new List<ViewModel>();

            for (int i = 0;i<saleUsers.Count;i++)
            {
                double sex = 100;
                double job = 100;
                double salery = Math.Pow(saleUsers[i].User.Salery - user.Salery, 2);
                if (saleUsers[i].User.Sex == user.Sex)
                {
                    sex = 0;
                }
                if (saleUsers[i].User.Job == user.Job)
                {
                    job = 0;
                }
                double age = Math.Pow(saleUsers[i].User.Age - user.Age, 2);
                double dist = Math.Sqrt(sex + job + salery + age);
                if(dist != 0)
                viewModel.Add(new ViewModel{Distance = dist,User= saleUsers[i].User });
            }
            viewModel.OrderBy(x => x.Distance).Take(3);
            
            List<Product> products = new List<Product>();

            foreach (var item in viewModel)
            {
                var sale = _saleRepository.GetByUser(item.User);

                products.AddRange(sale.ShoppingCart.ProductList);
            }
            var a = products.Distinct();
            return View(products);
        }
    } 
}