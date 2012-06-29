using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Shipper;

namespace Store.Domain.Entities
{
    public class ShipperMetadata
    {
        [HiddenInput(DisplayValue = false)]
        public int ShipperID { get; set; }

        [Required(ErrorMessageResourceName = "CompanyName_validation_error_message", ErrorMessageResourceType = typeof(ShipperStrings))]
        [Display(Name = "CompanyName", ResourceType = typeof(ShipperStrings))]
        public string CompanyName { get; set; }

        [Required(ErrorMessageResourceName = "Phone_validation_error_message", ErrorMessageResourceType = typeof(ShipperStrings))]
        [Display(Name = "Phone", ResourceType = typeof(ShipperStrings))]
        public string Phone { get; set; }
    }
}
