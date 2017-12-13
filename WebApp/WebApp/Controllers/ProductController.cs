using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository _productRepository;
        private SaleRepository _saleRepository;
        // GET: Product
        public ProductController()
        {
            _productRepository = new ProductRepository();
            _saleRepository = new SaleRepository();
        }
        public void PrepareDropdownListData()
        {
            List<SelectListItem> process = new List<SelectListItem>();
            process.Add(new SelectListItem{Text ="Delete", Value ="0"});
            process.Add(new SelectListItem { Text = "Search", Value = "1" });

            List<SelectListItem> byx = new List<SelectListItem>();
            byx.Add(new SelectListItem{Text = "Name",Value = "0"});
            byx.Add(new SelectListItem { Text = "Brand", Value = "1" });
            byx.Add(new SelectListItem { Text = "Model", Value = "2" });
            byx.Add(new SelectListItem { Text = "Word", Value = "3" });

            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var item in _productRepository.CategoryHash.Keys)
            {
                categoryList.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }          

            ViewData["process"] = process;
            ViewData["byx"] = byx;
            ViewData["category"] = categoryList;

        }
        public ActionResult Index(List<Product> products)
        {
            PrepareDropdownListData();
            return View(_productRepository.GetAllProduct());
        }
        [HttpPost]
        public ActionResult Index(string process,string byx , string text)
        {
            PrepareDropdownListData();
            List<Product> products = null;
            switch (process)
            {
                case "0":
                    switch (byx)
                    {

                        case "0":
                        
                            break;
                        case "1":
                            
                            break;
                        case "2":
                     
                            break;
                        case "3":
                           
                            break;

                    }
                    break;
                case "1":
                    switch (byx)
                    {

                        case "0":
                            
                            break;
                        case "1":
                          
                            break;
                        case "2":
                         
                            break;
                        case "3":
                            products =_productRepository.GetProductByWord(text);
                            break;

                    }
                    break;

            }
            return View(products);
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            PrepareDropdownListData();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product,string newCategory)
        {
            try
            {
                product.Id = Guid.NewGuid().ToString();
                if (newCategory != null)
                {
                    _productRepository.AddCategory(newCategory);
                    product.Category = newCategory;
                }
                _productRepository.Add(product);
                
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            return View(_productRepository.Get(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
               
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Sales()
        {
            List<Sale> sales = _saleRepository.GetAll();
            return View(sales);
        }
    }
}
