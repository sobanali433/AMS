using Microsoft.AspNetCore.Mvc.Rendering;

namespace AMS.Models
{
    public class UserViewModel
    {
        public int UserMasterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public short? Gender { get; set; }
        public string Ip { get; set; } = null!;

        public int RoleId { get; set; }

        public List<SelectListItem> RoleList { get; set; }

    }
}
