using AMS.Data;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class BranchModel
    {
        public int BranchId { get; set; }
        [Required]

        public string BranchName { get; set; }
        public string Location { get; set; }
        public bool IsEdit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedAtOnString { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<UserMaster> UserMasters { get; set; }
    }
}
