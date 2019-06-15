using Chart_Leader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chart_Leader.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected leaderEntities db = new leaderEntities();
        protected bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 5242880) && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        // GET: Error
        public ActionResult FileUploadLimitExceeded()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        //
        public ActionResult Error()
        {
            return View();
        }//
      

        public BaseController()
        {
            ViewBag.category = new SelectList(db.Categories.ToList(), "Cat_id", "Cat_Name");
            ViewBag.lastProducts = db.Products.OrderByDescending(x => x.Product_id).Take(3).ToList<Products>();
        }

    }
}