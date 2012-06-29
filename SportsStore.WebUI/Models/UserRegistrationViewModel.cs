using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.User;
using Store.Resourses.Registration;
using Microsoft.Web.Mvc;

namespace Store.WebUI.Models
{
    public class UserRegistrationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [Required(ErrorMessageResourceName = "Password_validation_error_message", ErrorMessageResourceType = typeof(UserStrings))]
        [Display(Name = "Password", ResourceType = typeof(UserStrings))]
        [DataType(DataType.Password)]
        [MinLength(2, ErrorMessageResourceName = "Password_lenght_error_message", ErrorMessageResourceType = typeof(RegistrationStrings))]
        public string Password { get; set; }

        [Display(Name = "Confirm_Password", ResourceType = typeof(RegistrationStrings))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName="Confirm_password_not_equal", ErrorMessageResourceType=typeof(RegistrationStrings))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "Email_validation_error_message", ErrorMessageResourceType = typeof(UserStrings))]
        [Display(Name = "Email", ResourceType = typeof(UserStrings))]
        [EmailAddress]
        public string Email { get; set; }
    }
}