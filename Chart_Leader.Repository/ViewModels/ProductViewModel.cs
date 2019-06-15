using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart_Leader.Repository.ViewModels
{
    public class ProductViewModel
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


        public virtual CategorieViewModel Categories { get; set; }
    }
}
