using Chart_Leader.Controllers;
using Chart_Leader.Models;
using Chart_Leader.Repository;
using Chart_Leader.Repository.RepositoryS_;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Chart_Leader.Areas.Users.Controllers
{
    [HandleError]
    public class HomeController : BaseController
    {
       IRepository<Categories> categoriesRepository;
        IRepository<Products> productsRepository;
        leader_Entities db = new leader_Entities();


        public HomeController(IRepository<Products> productsRepository, IRepository<Categories> categoriesRepository)
        {
            this.productsRepository = productsRepository;
            this.categoriesRepository = categoriesRepository;

        }


        public async Task<ActionResult>  Index(string ProductName = "")
        {
            List<Products> listDM;
            if (ProductName == "")
            {
                listDM = productsRepository.GetAll().ToList<Products>();
            }
            else
            {
                SqlParameter param = new SqlParameter("@search", ProductName.Trim() ?? (object)DBNull.Value);

                listDM =await db.Database.SqlQuery<Products>("SearchProductsByName @search", param).ToListAsync();
            }
            List<ProductsViewModel> listVM = new List<ProductsViewModel>();
            AutoMapper.Mapper.Map(listDM, listVM);
            ViewBag.category = new SelectList(categoriesRepository.GetAll(), "Cat_id", "Cat_Name");
            return View(listVM);
        }


        public async Task<ActionResult> GetAllProducts(string ProductName = "")
        {
            List<Products> listDM;
            if (ProductName == "")
            {
                listDM =  productsRepository.GetAll().ToList<Products>();
            }
            else
            {
                SqlParameter param = new SqlParameter("@search", ProductName.Trim() ?? (object)DBNull.Value);

                listDM = await db.Database.SqlQuery<Products>("SearchProductsByName @search", param).ToListAsync();
            }
            List<ProductsViewModel> listVM = new List<ProductsViewModel>();
            AutoMapper.Mapper.Map(listDM, listVM);
            ViewBag.category = new SelectList(categoriesRepository.GetAll(), "Cat_id", "Cat_Name");
            return PartialView(listVM);
        }


        public async Task<ActionResult> Category(string catName)
        {
            List<Products> products;
            if (catName == "")
            {
                products =   productsRepository.GetAll().ToList<Products>();
            }
            else
            {
                products = productsRepository.GetAll().Where(p => p.Categories.Cat_Name == catName).ToList<Products>();
            }
            return View("Index", products);
        }

        public async Task<ActionResult> ProductSearch(string ProductName)
        {
            List<Products> products;
            if (ProductName == "")
            {
                products = productsRepository.GetAll().ToList<Products>();
            }
            else
            {
                SqlParameter param = new SqlParameter("@search", ProductName.Trim() ?? (object)DBNull.Value);

                products = await db.Database.SqlQuery<Products>("SearchProductsByName @search", param).ToListAsync();
            }
            return View("Index", products);
        }
        // GET: Products
        public async Task<ActionResult> Cat(int id)
        {
            var products =   db.Products.Where(x => x.Cat_id == id);
            return View(products.ToList());
        }


        // GET: Products/Details/5
        public async Task<ActionResult> Product(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}