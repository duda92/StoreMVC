using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Supplier;
using Microsoft.Web.Mvc;

namespace Store.Domain.Entities
{
    public class SupplierMetadata
    {
        [HiddenInput(DisplayValue = false)]
        public int SupplierID { get; set; }

        [Required(ErrorMessageResourceName = "CompanyName_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "CompanyName", ResourceType = typeof(SupplierStrings))]
        public string CompanyName { get; set; }

        //[Required(ErrorMessageResourceName = "CompanyName_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "CompanyName", ResourceType = typeof(SupplierStrings))]
        public string ContactName { get; set; }

        //[Required(ErrorMessageResourceName = "ContactTitle_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "ContactTitle", ResourceType = typeof(SupplierStrings))]
        public string ContactTitle { get; set; }

        //[Required(ErrorMessageResourceName = "Address_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "Address", ResourceType = typeof(SupplierStrings))]
        public string Address { get; set; }

        //[Required(ErrorMessageResourceName = "City_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "City", ResourceType = typeof(SupplierStrings))]
        public string City { get; set; }

        //[Required(ErrorMessageResourceName = "Region_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "Region", ResourceType = typeof(SupplierStrings))]
        public string Region { get; set; }

        //[Required(ErrorMessageResourceName = "PostalCode_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "PostalCode", ResourceType = typeof(SupplierStrings))]
        public string PostalCode { get; set; }

        //[Required(ErrorMessageResourceName = "Country_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "Country", ResourceType = typeof(SupplierStrings))]
        public string Country { get; set; }

        //[Required(ErrorMessageResourceName = "Phone_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "Phone", ResourceType = typeof(SupplierStrings))]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        //[Required(ErrorMessageResourceName = "CompanyName_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "Fax", ResourceType = typeof(SupplierStrings))]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        //[Required(ErrorMessageResourceName = "HomePage_validation_error_message", ErrorMessageResourceType = typeof(SupplierStrings))]
        [Display(Name = "HomePage", ResourceType = typeof(SupplierStrings))]
        [Url]
        public string HomePage { get; set; }
    }
}
