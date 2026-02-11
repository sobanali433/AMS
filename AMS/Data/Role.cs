
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMS.Data
{
    public class Role
    {
        [Key]

        public short RoleId { get; set; }

        [Column(TypeName = "nvarchar(50)")]

        public string? RoleName { get; set; }

        public virtual ICollection<UserMaster> UserMasters { get; set; } = new List<UserMaster>();
    }
}
