using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Customer;

namespace Store.WebUI.Models
{
    public class CustomerMainViewModel
    {
        [Required(ErrorMessageResourceName = "CustomerID_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "CustomerID", ResourceType = typeof(CustomerStrings))]
        [MaxLength(5, ErrorMessageResourceName = "CustomerID_Lenght_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        public string CustomerID { get; set; }

        [Required(ErrorMessageResourceName = "CompanyName_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "CompanyName", ResourceType = typeof(CustomerStrings))]
        public string CompanyName { get; set; }

        [Required(ErrorMessageResourceName = "ContactName_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "ContactName", ResourceType = typeof(CustomerStrings))]
        public string ContactName { get; set; }

        [Required(ErrorMessageResourceName = "ContactTitle_validation_error_message", ErrorMessageResourceType = typeof(CustomerStrings))]
        [Display(Name = "ContactTitle", ResourceType = typeof(CustomerStrings))]
        public string ContactTitle { get; set; }        
    }
}