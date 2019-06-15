using Chart_Leader.Models;
using Chart_Leader.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Chart_Leader.Areas.Admin.Controllers
{
    public class Products_HttpClientController : Controller
    {
        leader_Entities db = new leader_Entities();
        // GET: Admin/Products_HttpClient
        public ActionResult Index()
        {
            List<Products> ProductsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Products").Result;
            ProductsList = response.Content.ReadAsAsync<List<Products>>().Result;
            return View(ProductsList);

           
        }

        public ActionResult AddEditProduct(int Product_id=0)
        {
            if (Product_id == 0)
            {
                ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name");
                return View(new Products());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Products/" + Product_id.ToString()).Result;
                Products Product = response.Content.ReadAsAsync<Products>().Result;
                ViewBag.Cat_id = new SelectList(db.Categories, "Cat_id", "Cat_Name", Product.Cat_id);

                return View(Product);
            }
        }

        [HttpPost]
        public ActionResult AddEditProduct(Products product)
        {
            if (product.Product_id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Products", product).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Products/" + product.Product_id, product).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int? id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Products/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }


      
    }
}