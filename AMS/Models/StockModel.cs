using AMS.Data;

namespace AMS.Models
{
    public class StockModel
    {

        public int StockId { get; set; }

        public int ProductId { get; set; }
        public Product Products { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public BranchMaster BranchMasters { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
