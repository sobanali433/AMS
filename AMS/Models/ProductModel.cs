using AMS.Data;
using AMS.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class ProductModel
    {

        public int ProductId { get; set; }
        [DisplayName("product name")]
        [Required(ErrorMessage = "Please specify product")]

        public string ProductName { get; set; }

        public ProductType ProductType { get; set; }

        [Required(ErrorMessage = "Please specify sku")]
        public string SKU { get; set; }

        [DisplayName("price")]
        [Required(ErrorMessage = "Please specify price")]

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }

        public DateTime CreatedAt { get; set; }
        [Required]

        public bool IsActive { get; set; } = true;
    }
}
