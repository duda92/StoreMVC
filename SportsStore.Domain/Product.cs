//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Store.Domain
{
    public partial class Product
    {
        public Product()
        {
            this.Order_Details = new HashSet<Order_Detail>();
        }
    
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<Order_Detail> Order_Details { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
    
}
