using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chart_Business_Layers;
using Chart_Business_Layers.Interface;
using Chart_Leader.Controllers;
using Chart_Leader.Models;
using Chart_Leader.Repository;
using Chart_Leader.Repository.RepositoryS_;
using Chart_Leader.Repository.ViewModels;

namespace Chart_Leader.Areas.Admin.Controllers

{
    //[Authorize(Roles = "admin")]
    //[HandleError]
    public class ProductsController : Controller
    {
        IRepository<Products> productsRepository;
        IRepository<Categories> categoriesRepository;
        IproductsBusiness iproductsBusiness;
        List<ProductViewModel> listProductsVM = new List<ProductViewModel>();
        ProductViewModel productvm = new ProductViewModel();


        public ProductsController(IproductsBusiness iproductsBusiness, IRepository<Products> productsRepository, IRepository<Categories> categoriesRepository)
        {
            this.productsRepository = productsRepository;
            this.categoriesRepository = categoriesRepository;
            this.iproductsBusiness = iproductsBusiness;
        }
        [AllowAnonymous]

        // GET: Products
        public ActionResult AllProducts()
        {
            List<Products> productslist = iproductsBusiness.GetAllProducts();
            AutoMapper.Mapper.Map(productslist, listProductsVM);
            return View(listProductsVM);
        }
      
        [AllowAnonymous]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            Products product = iproductsBusiness.GetProductByID(id);
            AutoMapper.Mapper.Map(product, productvm);
            return View(productvm);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Cat_id = new SelectList(categoriesRepository.GetAll(), "Cat_id", "Cat_Name");
            return View();
        }
        [HttpPost]


        public ActionResult AddProduct(ProductsViewModel productvm, HttpPostedFileBase ImageUpload)
        {
            int lastProductID = iproductsBusiness.GetAllProducts().ToList<Products>().OrderByDescending(x => x.Product_id)
                .Select(x => x.Product_id).FirstOrDefault() + 1;
            if (ImageUpload != null)
            {
                if (ValidateFile(ImageUpload))
                {
                    try
                    {
                        string imgProductName = productvm.Cat_id + "-" + lastProductID + ".jpg";
                        productvm.Product_Image = "~/Common/Images/" + imgProductName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/Images/"), imgProductName));
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("FileName", "Sorry an error occurred saving the file to disk, please try again");

                    }
                }
                else
                {
                    ModelState.AddModelError("FileName", "The file must be gif, png, jpeg or jpg and less than 5MB in size");
                }
            }
            else
            {
                productvm.Product_Image = "~/Common/Images/uploade.jpg";
                //if the user has not entered a file return an error message
                // ModelState.AddModelError("FileName", "Please choose a file");
            }


            ViewBag.Cat_id = new SelectList(categoriesRepository.GetAll(), "Cat_id", "Cat_Name", productvm.Cat_id);

            Products product = new Products();
            AutoMapper.Mapper.Map(productvm, product);
            iproductsBusiness.Add(product);
            TempData["SuccessMessage"] = "Add Successfully";
            return RedirectToAction("AllProducts");
        }
        // GET: Products/Create
        public ActionResult Edit(int? id)
        {
            Products product = new Products();
            if (id != null)
            {
                product = iproductsBusiness.GetProductByID(id);
            }
            ProductsViewModel productvm = new ProductsViewModel();
            AutoMapper.Mapper.Map(product, productvm);
            ViewBag.Cat_id = new SelectList(categoriesRepository.GetAll(), "Cat_id", "Cat_Name", product.Cat_id);

            return View("Edit", productvm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsViewModel productvm, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null)
            {
                string imgProductName = productvm.Cat_id + "-" + productvm.Product_id + ".jpg";
                productvm.Product_Image = "~/Common/Images/" + imgProductName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/Images/"), imgProductName));
            }
            else
            {
                productvm.Product_Image = "~/Common/Images/uploade.jpg";
            }

            Products product = new Products();
            AutoMapper.Mapper.Map(productvm, product);
            iproductsBusiness.Update(product);
            TempData["SuccessMessage"] = "Update Successfully";

            return RedirectToAction("AllProducts");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            Products product = iproductsBusiness.GetProductByID(id);
            AutoMapper.Mapper.Map(product, productvm);
            return View(productvm);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteProduct(int? id)
        {
            iproductsBusiness.Delete(id);
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("AllProducts");
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
    }
}
