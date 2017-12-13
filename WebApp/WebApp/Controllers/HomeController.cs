using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class HomeController : Controller
        
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Exit()
        {
            Session["User"] = null;
            return RedirectToAction("Index");
        }
    }
}