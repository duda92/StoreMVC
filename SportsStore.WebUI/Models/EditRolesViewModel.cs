using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;

namespace Store.WebUI.Models
{
    public class EditRolesViewModel
    {
        public int UserId { get; set; }

        public List<bool> IsInRoles { get; set; } 

    }
}