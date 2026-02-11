using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AMS.Data
{
    public class UserMaster
    {
        [Key]

        public int UserMasterId { get; set; }
        [Column(TypeName = "varchar(20)")]

        public string Username { get; set; } = null!;
        [Column(TypeName = "varchar(500)")]

        public string UserPassword { get; set; } = null!;
        [Column(TypeName = "varchar(max)")]

        public string UserMasterPassword { get; set; } = null!;
        [Column(TypeName = "varchar(100)")]

        public string? FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]

        public string? LastName { get; set; }
        [Column(TypeName = "varchar(20)")]

        public string ContactNumber { get; set; } = null!;

        public short RoleId { get; set; }


        public bool IsFirstTimeLogin { get; set; }

        public bool IsActive { get; set; }


        public DateTime? DateOfBirth { get; set; }

        public short? Gender { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }
        [Column(TypeName = "varchar(30)")]

        public string? Ip { get; set; }
        public virtual Role Roles { get; set; } = null!;

        public int BranchId { get; set; }
        public BranchMaster BranchMasters { get; set; }



    }
}