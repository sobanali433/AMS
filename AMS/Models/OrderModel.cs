using AMS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        public int BranchId { get; set; }
        public BranchMaster BranchMasters { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        public List<SelectListItem> ProductList { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Product Type is required")]

        public string OrderType { get; set; }

        public int CreatedById { get; set; }
        public UserMaster CreatedBy { get; set; }
        public bool IsEdit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
}
