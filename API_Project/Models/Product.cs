//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int Cat_id { get; set; }
        public int Product_id { get; set; }
        public string Product_Name { get; set; }
        public decimal Product_Price { get; set; }
        public int Product_QTY { get; set; }
        public Nullable<System.DateTime> Product_Writing_Date { get; set; }
        public string Product_Description { get; set; }
        public string Product_Image { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
