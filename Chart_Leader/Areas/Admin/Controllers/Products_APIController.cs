using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chart_Leader.Areas.Admin.Controllers
{
    public class Products_APIController : Controller
    {
        leaderEntities_API db = new leaderEntities_API();
        // GET: Admin/Products_API
        public ActionResult Create()
        {
            return View();
        }

        // GET: Admin/Products_Api_Jquery
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetListProductsJSON()
        {
            List<Product> productslist = db.Products.ToList();
            return Json(new { data = productslist }, JsonRequestBehavior.AllowGet);
        }
    }
}