using Chart_Leader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart_Business_Layers;
using Chart_Business_Layers.Interface;
using Chart_Leader.Repository;
using Chart_Leader.Controllers;
using Newtonsoft.Json;
using Chart_Leader.Repository.RepositoryS_;
using System.IO;

namespace Chart_Leader.Areas.Admin.Controllers
{
    //[RouteArea("Admin")]
    //[RoutePrefix("Categories")]
    //[Route("{action}")]
    public class AdminController : BaseController
    {
        //private GenericUnitOfWork unitOfWork = new GenericUnitOfWork();
        IRepository<Categories> categoriesRepository;
        public AdminController(IRepository<Categories> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }



         //GET: Admin/Admin
        public ActionResult Dashboard()
        {
            return View();
        }
        //[Route("GetAll")]
        public ActionResult GetAllCategories()
        {
            List<Categories> listDM = categoriesRepository.GetAll().ToList<Categories>();
            List<CategoriesViewModel> listVM = new List<CategoriesViewModel>();
            AutoMapper.Mapper.Map(listDM, listVM);

            return View(listVM);
        }

        // GET: Add Categories/Create
        [HttpGet]
        public ActionResult AddCategorie()
        {

            return View();
        }
        [HttpPost]


        public ActionResult AddCategorie(CategoriesViewModel categories, HttpPostedFileBase ImageUpload)
        {
            int idCat = db.Categories.OrderByDescending(x => x.Cat_id).Select(x => x.Cat_id).FirstOrDefault() + 1;

            if (ImageUpload != null)
            {
                if (ValidateFile(ImageUpload))
                {
                    try
                    {
                        string imgCatName = idCat + ".jpg";
                        categories.Cat_Image = "~/Common/CatImages/" + imgCatName;
                        ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/CatImages/"), imgCatName));
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
                categories.Cat_Image = "~/Common/Images/uploade.jpg";
                //if the user has not entered a file return an error message
                // ModelState.AddModelError("FileName", "Please choose a file");
            }

            Categories cat = new Categories();
            AutoMapper.Mapper.Map(categories, cat);
            categoriesRepository.Add(cat);
            return RedirectToAction("GetAllCategories");
        }

        //[Route("~/UpdateCategory/{id:int}")]
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            Categories cat = new Categories();
            if (id != null)
            {
                cat = categoriesRepository.GetByID(id);
            }
            CategoriesViewModel cvm = new CategoriesViewModel();
            AutoMapper.Mapper.Map(cat, cvm);
            return View("UpdateCategory", cvm);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoriesViewModel cvm, HttpPostedFileBase ImageUpload)
        {
            string imgName = cvm.Cat_id + ".jpg";
            cvm.Cat_Image = "~/Common/CatImages/" + cvm.Cat_id + ".jpg";
            ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Common/CatImages/"), imgName));

            Categories cat = new Categories();
            AutoMapper.Mapper.Map(cvm, cat);


            categoriesRepository.Update(cat);
            return RedirectToAction("GetAllCategories");
        }

        //Detailes
        public ActionResult DetailCategory(int id)
        {
            Categories cat = new Categories();
            if (id != null)
            {
                cat = categoriesRepository.GetByID(id);
            }
            CategoriesViewModel cvm = new CategoriesViewModel();
            AutoMapper.Mapper.Map(cat, cvm);
            return View(cvm);
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            Categories cat = new Categories();
            if (id != null)
            {
                cat = categoriesRepository.GetByID(id);
            }
            CategoriesViewModel cvm = new CategoriesViewModel();
            AutoMapper.Mapper.Map(cat, cvm);
            return View(cvm);
        }

        [HttpPost, ActionName("DeleteCategory")]

        public ActionResult Delete(int id)
        {
            //Categories cat = categoriesRepository.GetByID(id);
            categoriesRepository.RemovebyWhereClause(x => x.Cat_id == id);
            return RedirectToAction("GetAllCategories");
        }

        //

    }
}