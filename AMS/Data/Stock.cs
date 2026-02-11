using System.ComponentModel.DataAnnotations;

namespace AMS.Data
{
    public class Stock
    {
        [Key]

        public int StockId { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        public int BranchId { get; set; }
        public BranchMaster BranchMasters { get; set; }

        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
