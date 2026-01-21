using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AMS.Models
{
    public class UserMasterModel
    {
        public int UserMasterId { get; set; }
        public string EncryptUserMasterId { get; set; }

        [DisplayName("Full Name")]
        public string UserFullName { get; set; }
        [DisplayName("User Name")]
        [StringLength(100)]
        [Required(ErrorMessage = "Please specify username")]
        public string Username { get; set; }
        [DisplayName("Old Password")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please specify old password")]
        public string OldPassword { get; set; }
        [Required]
        //[DataType(DataType.Password)]
        public string Userpassword { get; set; }
        [DisplayName("Confirm Password")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please specify confirm password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        [DisplayName("Role")]
        [Required(ErrorMessage = "Please select role")]
        public short RoleId { get; set; }
        public string EncryptUserRoleId { get; set; }
        [DisplayName("Role")]
        public string RoleName { get; set; }
        //[DisplayName("Last Name")]
        //[StringLength(100)]
        //[Required(ErrorMessage = "Please specify last name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        [StringLength(100)]
        [Required(ErrorMessage = "Please specify fast name")]
        public string FirstName { get; set; }
        [DisplayName("Contact Number")]
        [StringLength(20)]
        [Required(ErrorMessage = "Please specify contactnumber")]
        public string ContactNumber { get; set; }
        [DisplayName("Email")]
        [StringLength(80)]
        [Required(ErrorMessage = "Please specify Email")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [DisplayName("Status")]
        public bool IsActive { get; set; }
        [DisplayName("Customers")]
        public int[] CustomerIds { get; set; }
        [DisplayName("Last Visited")]
        public DateTime? LastVisited { get; set; }
        [DisplayName("Date Added")]
        public DateTime CreatedDate { get; set; }

        //[DisplayName("Name")]
        //[Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public short? Gender { get; set; }

        public string FullName { get; set; }
        public bool IsFirstTimeLogin { get; set; }

        public List<SelectListItem> RoleList { get; set; }

        public string CaptchaCode { get; set; }
        public string CreatedOnString { get; set; }
        public string? Ip { get; set; }

        public string CaptchaImage { get; set; }

        //[Display(Name = "Attendance Type"), Required(ErrorMessage = "please specify attendance type")]
        //public short AttendanceType { get; set; }
        public class CaptchaResult
        {
            public string CaptchaCode { get; set; }
            public string CaptchaBase64 { get; set; } // "data:image/png;base64,..."
        }
    }
}

