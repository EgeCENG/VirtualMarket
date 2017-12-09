using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        // GET: Product
        public void PrepareDropdownListData()
        {
            List<SelectListItem> process = new List<SelectListItem>();
            process.Add(new SelectListItem{Text ="Delete", Value ="0"});
            process.Add(new SelectListItem { Text = "Search", Value = "1" });

            List<SelectListItem> byx = new List<SelectListItem>();
            byx.Add(new SelectListItem{Text = "Name",Value = "0"});
            byx.Add(new SelectListItem { Text = "Brand", Value = "1" });
            byx.Add(new SelectListItem { Text = "Model", Value = "2" });

            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var item in productRepository.categoryHash.Keys)
            {
                categoryList.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }          

            ViewData["process"] = process;
            ViewData["byx"] = byx;
            ViewData["category"] = categoryList;

        }
        public ActionResult Index()
        {
            PrepareDropdownListData();
            return View(productRepository.GetAllProduct());
        }
        [HttpPost]
        public ActionResult Execute(string process,string byx , string text)
        {
            Func<Product, bool> exp = null;
            switch (byx)
            {
                   
                case "0":
                    exp = x => x.Name == text;
                    break;
                case "1":
                    exp = x => x.Brand == text;
                    break;
                case "2":
                    exp = x => x.Model == text;
                    break;

            }
            switch (process)
            {
                case "0":
                    productRepository.DeleteBy(exp);
                    break;
                case "1":
                    return View(productRepository.SearchBy(exp));
              
            }
            return RedirectToAction("Index");
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            PrepareDropdownListData();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                product.Id = Guid.NewGuid().ToString();
                productRepository.Add(product);
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
            return View(productRepository.Get(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
               
                productRepository.Update(product);
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
            productRepository.Delete(id);
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
    }
}
