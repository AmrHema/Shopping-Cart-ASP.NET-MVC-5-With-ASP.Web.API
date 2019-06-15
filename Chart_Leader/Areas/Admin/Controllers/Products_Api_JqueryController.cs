using API_Project.Models;
using Chart_Leader.Models;
using Chart_Leader.Repository;
using Chart_Leader.Repository.RepositoryS_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Chart_Leader.Areas.Admin.Controllers
{
    public class Products_Api_JqueryController : Controller
    {

        IRepository<Products> productsRepository;
        IRepository<Categories> categoriesRepository;
        private leader_Entities dbcontext = new leader_Entities();
        leaderEntities_API db = new leaderEntities_API();
        public Products_Api_JqueryController(IRepository<Products> productsRepository, IRepository<Categories> categoriesRepository)
        {
            this.productsRepository = productsRepository;
            this.categoriesRepository = categoriesRepository;
        }


        // GET: Admin/Products_Api_Jquery
        public ActionResult Index()
        {
            ViewBag.Cat_id = new SelectList(dbcontext.Categories, "Cat_id", "Cat_Name");
            return View();
        }



        //autocomplete
        public JsonResult GetProducts(string term)
        {
            List<string> products = productsRepository.GetAll().Where(s => s.Product_Name.ToLower().StartsWith(term.Trim().ToLower()))
                .Select(x => x.Product_Name).ToList().Take(5).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetListProducts2()
        {

            return Json(new { data = dbcontext.Products.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ProductPartial(int Product_id=0)
        {
            Products product;
            if (Product_id == 0)
            {
                ViewBag.Cat_id = new SelectList(dbcontext.Categories, "Cat_id", "Cat_Name");
                product = new Products();
            }
            else
            {
                 product = dbcontext.Products.Where(x => x.Product_id == Product_id).FirstOrDefault();
                ViewBag.Cat_id = new SelectList(dbcontext.Categories, "Cat_id", "Cat_Name", product.Cat_id);
            }
            return PartialView(product);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbcontext.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}