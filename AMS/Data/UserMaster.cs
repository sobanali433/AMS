using System.Data;

namespace AMS.Data
{
    public class UserMaster
    {
        public int UserMasterId { get; set; }

        public string Username { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public string UserMasterPassword { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

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

        public string Ip { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;



    }
}
