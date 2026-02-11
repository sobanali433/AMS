using System.ComponentModel.DataAnnotations;

namespace AMS.Data
{
    public class BranchMaster
    {
        [Key]
        public int BranchId { get; set; }
        [Required]

        public string BranchName { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }   

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<UserMaster> UserMasters { get; set; }

    }
}
