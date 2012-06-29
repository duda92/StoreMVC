using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Order;
using Store.Resourses.Shipper;

namespace Store.Domain.Entities
{
    public class OrderMatadata
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderID { get; set; }

        [Required(ErrorMessageResourceName = "CustomerID_validation_error_message", ErrorMessageResourceType = typeof(OrderStrings))]
        [Display(Name = "CustomerID", ResourceType = typeof(OrderStrings))]
        public string CustomerID { get; set; }

        [Display(Name = "OrderDate", ResourceType = typeof(OrderStrings))]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [Display(Name = "RequiredDate", ResourceType = typeof(OrderStrings))]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> RequiredDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ShippedDate { get; set; }

        [Required(ErrorMessageResourceName = "ShipAddress_validation_error_message", ErrorMessageResourceType = typeof(OrderStrings))]
        [Display(Name = "ShipAddress", ResourceType = typeof(OrderStrings))]
        public string ShipAddress { get; set; }

        [Required(ErrorMessageResourceName = "ShipCity_validation_error_message", ErrorMessageResourceType = typeof(OrderStrings))]
        [Display(Name = "ShipCity", ResourceType = typeof(OrderStrings))]
        public string ShipCity { get; set; }

        [Display(Name = "ShipRegion", ResourceType = typeof(OrderStrings))]
        public string ShipRegion { get; set; }

        [Required(ErrorMessageResourceName = "ShipCountry_validation_error_message", ErrorMessageResourceType = typeof(OrderStrings))]
        [Display(Name = "ShipCountry", ResourceType = typeof(OrderStrings))]
        public string ShipCountry { get; set; }

        [Required(ErrorMessageResourceName = "ShipperID_validation_error_message", ErrorMessageResourceType = typeof(ShipperStrings))]
        [Display(Name = "ShipperID", ResourceType = typeof(ShipperStrings))]
        public int ShipperID { get; set; }


    }
}
