using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        Database db = new Database();
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

            ViewData["process"] = process;
            ViewData["byx"] = byx;

        }
        public ActionResult Index()
        {
            List<Product> products = db.Deserialize();
            PrepareDropdownListData();
            return View(products);
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
                    db.DeleteBy(exp);
                    break;
                case "1":
                    return View(db.SearchBy(exp));
              
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                product.Id = Guid.NewGuid().ToString();
                db.Add(product);
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
            return View(db.Get(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
               
                db.Update(product);
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
            db.Delete(id);
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
