using System.ComponentModel.DataAnnotations;
using School.Models;

namespace School.DTOs.RegisterAndLoginDTOs
{
    public class MainRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Phone { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}
