using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.Product;
using Lucene.Net.Documents;

namespace Store.Domain.Entities
{
    public class ProductMetadata
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Required(ErrorMessageResourceName = "product_name_validation_error_message", ErrorMessageResourceType = typeof(ProductStrings))]
        [Display(Name = "product_name", ResourceType = typeof(ProductStrings))]
        public string ProductName { get; set; }

        /// <summary>
        /// The supplier ID.
        /// </summary>
        [Display(Name = "supplier", ResourceType = typeof(ProductStrings))]
        public int SupplierID { get; set; }

        /// <summary>
        /// The category ID.
        /// </summary>
        [Display(Name = "category", ResourceType = typeof(ProductStrings))]
        public int CategoryID { get; set; }

        /// <summary>
        /// QuantityPerUnit
        /// </summary>
        [Display(Name = "quantity_per_unit", ResourceType = typeof(ProductStrings))]
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
       
        [Display(Name = "unit_price", ResourceType = typeof(ProductStrings))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Image of product
        /// </summary>
        [Display(Name = "image", ResourceType = typeof(ProductStrings))]
        public byte[] Image { get; set; }


        /// <summary>
        /// Description of product
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "product_description", ResourceType = typeof(ProductStrings))]
        public string Description { get; set; }
    }
}
