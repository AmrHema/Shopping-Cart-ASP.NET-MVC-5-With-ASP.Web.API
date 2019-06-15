
using Chart_Leader.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chart_Leader.Models
{
    public class ProductsDomaineModel
    {
        public int Cat_id { get; set; }
        public int Product_id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public int Product_QTY { get; set; }
        public Nullable<System.DateTime> Product_Writing_Date { get; set; }


        public string Product_Description { get; set; }
        public string Product_Image { get; set; }

        public virtual Categories Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Products> Order_Products { get; set; }
    }
}