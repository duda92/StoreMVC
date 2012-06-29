using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Category;
using System.Web;
using Lucene.Net.Documents;

namespace Store.Domain.Entities
{
    public class CategoryMetadata
    {
        [HiddenInput(DisplayValue = false)] 
        public int CategoryID { get; set; }

        [Required(ErrorMessageResourceName = "category_name_validation_error_message", ErrorMessageResourceType = typeof(CategoryStrings))]
        [Display(Name = "category_name", ResourceType = typeof(CategoryStrings))]
        public string CategoryName { get; set; }

        [Required(ErrorMessageResourceName = "description_validation_error_message", ErrorMessageResourceType = typeof(CategoryStrings))]
        [Display(Name = "description", ResourceType = typeof(CategoryStrings))]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

    }
}
