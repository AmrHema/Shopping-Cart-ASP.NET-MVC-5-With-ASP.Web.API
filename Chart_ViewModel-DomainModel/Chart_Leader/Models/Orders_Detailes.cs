//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chart_Leader.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders_Detailes
    {
        public Nullable<int> Product_id { get; set; }
        public int Order_id { get; set; }
        public int Order_detailes_id { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Discout { get; set; }
        public Nullable<decimal> TotalSale { get; set; }
        public Nullable<int> Cat_id { get; set; }
    
        public virtual Categories Categories { get; set; }
    }
}