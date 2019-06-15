using Chart_Leader.Models;
using Chart_Leader.Repository;
using Chart_Leader.Repository.RepositoryS_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace Chart_Leader.Areas.Admin.Controllers
{
    public class Products_JsonController : Controller
    {
        IRepository<Products> productsRepository;
        IRepository<Categories> categoriesRepository;
        private leader_Entities dbcontext = new leader_Entities();
        public Products_JsonController(IRepository<Products> productsRepository, IRepository<Categories> categoriesRepository)
        {
            this.productsRepository = productsRepository;
            this.categoriesRepository = categoriesRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetListProducts2()
        {
            List<Products> pro =  productsRepository.GetAll().ToList<Products>();
            // recordsTotal
            int totalrows = pro.Count;
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            //custom filtering
            if (!string.IsNullOrEmpty(Request["columns[0][search][value]"]))
                pro = pro.Where(x => x.Product_Name.ToLower().Contains(Request["columns[0][search][value]"].ToLower())).ToList<Products>();

            if (!string.IsNullOrEmpty(Request["columns[1][search][value]"]))
                pro = pro.Where(x => x.Product_Price.ToString().ToLower().Contains(Request["columns[1][search][value]"].ToLower())).ToList<Products>();

            if (!string.IsNullOrEmpty(Request["columns[2][search][value]"]))
                pro = pro.Where(x => x.Product_QTY.ToString().ToLower().Contains(Request["columns[2][search][value]"].ToLower())).ToList<Products>();


            if (!string.IsNullOrEmpty(Request["columns[3][search][value]"]))
                pro = pro.Where(x => x.Product_id.ToString().ToLower().Contains(Request["columns[3][search][value]"].ToLower())).ToList<Products>();

            // recordsFiltered
            int totalrowsafterfiltering = pro.Count;
            //sorting
            pro = pro.OrderBy(sortColumnName + " " + sortDirection).ToList<Products>();
            //paging
            pro = pro.Skip(start).Take(length).ToList<Products>();
            List<ProductsViewModel> listProductsVM = new List<ProductsViewModel>();
            AutoMapper.Mapper.Map(pro, listProductsVM);

            return Json(new { data = listProductsVM, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);

        }

        //autocomplete
        public JsonResult GetProducts(string term)
        {
            List<string> products = productsRepository.GetAll().Where(s => s.Product_Name.ToLower().StartsWith(term.Trim().ToLower()))
                .Select(x => x.Product_Name).ToList().Take(10).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }


        //[Route("Details/{id}")]

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = dbcontext.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
        public ActionResult AddEditProduct(int Product_id)
        {
            ProductsViewModel productsViewModel = new ProductsViewModel();

            Products Product;
            if (Product_id > 0)
            {
                Product = productsRepository.GetFirstorDefaultByParameter(x => x.Product_id == Product_id);
                ViewBag.Cat_id = new SelectList(dbcontext.Categories, "Cat_id", "Cat_Name", Product.Cat_id);

            }
            else
            {
                Product = new Products();
                ViewBag.Cat_id = new SelectList(dbcontext.Categories, "Cat_id", "Cat_Name");

            }
            AutoMapper.Mapper.Map(Product, productsViewModel);

            return PartialView("_AddEdit", productsViewModel);
        }


        public bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 5242880) && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        public ActionResult AddEditProduct(Products product, HttpPostedFileBase ImageUpload)
        {
            string imgProductName;
            if (product.Product_id == 0)                           
            {
                int lastProductID = productsRepository.GetAll().ToList<Products>().OrderByDescending(x => x.Product_id)
              .Select(x => x.Product_id).FirstOrDefault() + 1;

                if (ImageUpload != null)
                {
                    if (!ValidateFile(ImageUpload))
                    {
                        ModelState.AddModelError("FileName", "The file must be gif, png, jpeg or jpg and less than 5MB in size");
                    }
                    else
                    {
                        imgProductName = product.Cat_id + "-" + lastProductID + ".jpg";
                        product.Product_Image = "~/Common/Images/" + imgProductName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/Images/"), imgProductName));
                    }
                }
                else
                {
                    product.Product_Image = "~/Common/Images/uploade.jpg";
                }

                productsRepository.Add(product);
                int result = dbcontext.SaveChanges();
                return Json(new { success = true, msg = "Add Succeded" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (ImageUpload != null)
                {
                    imgProductName = product.Cat_id + "-" + product.Product_id + ".jpg";
                    product.Product_Image = "~/Common/Images/" + imgProductName;
                    ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/Images/"), imgProductName));
                }
                else
                {
                    product.Product_Image = "~/Common/Images/uploade.jpg";
                }

                productsRepository.Update(product);
                int result = dbcontext.SaveChanges();
                return Json(new { success = true, msg = "Update Succeded" }, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        public ActionResult DeleteProduct(int? Product_id)
        {
            if (Product_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = dbcontext.Products.Find(Product_id);
            if (products == null)
            {
                return HttpNotFound();
            }
            else
            {
                dbcontext.Products.Remove(products);
                dbcontext.SaveChanges();
            }
            return Json(new { success = true, msg = "Deleted Succeded" }, JsonRequestBehavior.AllowGet);
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