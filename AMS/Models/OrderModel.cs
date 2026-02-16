using AMS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public int Quantity { get; set; }

        public string OrderType { get; set; }

        public int CreatedById { get; set; }
        public UserMaster CreatedByUser { get; set; }
        public string CreatedBy { get; set; }
        public bool IsEdit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
}
