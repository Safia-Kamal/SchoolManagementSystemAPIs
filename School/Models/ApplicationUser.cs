using Microsoft.AspNetCore.Identity;

namespace School.Models
{
    public class ApplicationUser:IdentityUser
    {
        public Role Role { get; set; }
        public int RelatedId { get; set; }

    }
    public enum Role
    {
        Admin = 1,
        Student = 2,
        Teacher = 3,
        Parent = 4,
    }

}
