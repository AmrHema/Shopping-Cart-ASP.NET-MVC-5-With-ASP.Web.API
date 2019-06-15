using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Chart_Leader.Repository.RepositoryS_;
using Chart_Leader.Repository;
using System.Data.SqlClient;
using Chart_Business_Layers;

namespace Chart_Leader.Models
{
    public class ProductsViewModel
    {
        //[HiddenInput]
        [Display(Name = "Categorie ID")]
        public int Cat_id { get; set; }

        [Display(Name = "Product ID")]
        public int Product_id { get; set; }

        [Display(Name = "Name")]
        public string Product_Name { get; set; }

        [Display(Name = "Price")]
        public decimal Product_Price { get; set; }

        [Display(Name = "Quantity")]
        public int Product_QTY { get; set; }

        [Display(Name = "Date Annonce")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Product_Writing_Date { get; set; }

        [Display(Name = "Categorie Name")]
        public string Product_CatName { get; set; }

        [Display(Name = "Description")]
        public string Product_Description { get; set; }

        public string Product_Image { get; set; }


        public virtual CategoriesViewModel Categories { get; set; }





        //public IEnumerable<Products> ListOfProducts { get; set; }

        //public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        //leader_Entities context = new leader_Entities();
        //public ProductsViewModel CreateModel(string search)
        //{
        //    SqlParameter[] param = new SqlParameter[]{
        //        new SqlParameter("@search",search??(object)DBNull.Value)
        //    };
        //    IEnumerable<Products> data = context.Database.SqlQuery<Products>("SearchProductsByName @search", param).ToList();
        //    return new ProductsViewModel
        //    {
        //        ListOfProducts = data
        //    };
        //}

    }
}