using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Store.Resourses.OrderDetails;

namespace Store.Domain.Entities
{
    public class OrderDetailsMetadata
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        [Required(ErrorMessageResourceName = "Quantity_validation_error_message", ErrorMessageResourceType = typeof(OrderDetailsStrings))]
        [Display(Name = "Quantity", ResourceType = typeof(OrderDetailsStrings))]
        [Range(0, (double)short.MaxValue)]
        public short Quantity { get; set; }

    }
}
