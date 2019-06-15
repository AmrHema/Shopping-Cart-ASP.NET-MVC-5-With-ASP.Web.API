using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chart_Leader.Models
{
    [MetadataType(typeof(ProductsViewModel))]
    public partial class Products
    {
        //[FileExtensions(Extensions = "txt,doc,docx,pdf", ErrorMessage = "Please upload valid format")]
        //public HttpPostedFileBase file { get; set; }
    }
    public class ProductsViewModel
    {
        //[HiddenInput]
        public int Cat_id { get; set; }
        public int Product_id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public int Product_QTY { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Product_Writing_Date { get; set; }


        public string Product_Description { get; set; }
        public string Product_Image { get; set; }

        public virtual Categories Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Products> Order_Products { get; set; }
    }
}