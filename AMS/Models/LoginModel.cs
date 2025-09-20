using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class LoginModel
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        //public string UserPassword { get; set; }
        [MinLength(4)]
        [MaxLength(4)]
        public string CaptchaCode { get; set; }

        public string CaptchaImage { get; set; }
        public bool IsActive { get; set; }
    
        public string UserMasterId { get; set; }

        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public string SessionId { get; set; }

        public string DeviceId { get; set; }
    }
}

