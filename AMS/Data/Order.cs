using System.ComponentModel.DataAnnotations;

namespace AMS.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        public int BranchId { get; set; }
        public BranchMaster BranchMasters{ get; set; }

        public int Quantity { get; set; }

        public string OrderType { get; set; } 

        public int CreatedById { get; set; }
        public UserMaster CreatedByUser { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
