using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Customer;

namespace Store.WebUI.Models
{
    public class CustomerDetailViewModel
    {
        [Required(ErrorMessageResourceName = "Address_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "Address", ResourceType = typeof(CustomerStrings))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "City_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "City", ResourceType = typeof(CustomerStrings))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "Region_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "Region", ResourceType = typeof(CustomerStrings))]
        public string Region { get; set; }

        [Required(ErrorMessageResourceName = "PostalCode_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "PostalCode", ResourceType = typeof(CustomerStrings))]
        public string PostalCode { get; set; }

        [Required(ErrorMessageResourceName = "Country_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "Country", ResourceType = typeof(CustomerStrings))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceName = "Phone_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "Phone", ResourceType = typeof(CustomerStrings))]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "Fax_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "Fax", ResourceType = typeof(CustomerStrings))]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
    }
}