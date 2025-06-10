using System.ComponentModel.DataAnnotations;
using School.Models;

namespace School.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required, StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required, StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; }
    }
}
