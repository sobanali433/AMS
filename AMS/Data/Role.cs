namespace AMS.Data
{
    public class Role
    {

        public short RoleId { get; set; }

        public string? RoleName { get; set; }

        public virtual ICollection<UserMaster> UserMasters { get; set; } = new List<UserMaster>();
    }
}
