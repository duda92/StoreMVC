using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Role;

namespace Store.Domain.Entities
{
    public class RoleMetadata
    {
        [Required(ErrorMessageResourceName = "RoleName_validation_error_message", ErrorMessageResourceType = typeof(RoleStrings))]
        [Display(Name = "RoleName", ResourceType = typeof(RoleStrings))]
        public string RoleName { get; set; }
    }
}
