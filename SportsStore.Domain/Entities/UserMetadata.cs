using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.User;
using Microsoft.Web.Mvc;

namespace Store.Domain.Entities
{
    public class UserMetadata
    {
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [Required(ErrorMessageResourceName = "Password_validation_error_message", ErrorMessageResourceType = typeof(UserStrings))]
        [Display(Name = "Password", ResourceType = typeof(UserStrings))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "Email_validation_error_message", ErrorMessageResourceType = typeof(UserStrings))]
        [Display(Name = "Email", ResourceType = typeof(UserStrings))]
        [EmailAddress]
        public string Email { get; set; }
    }
}
