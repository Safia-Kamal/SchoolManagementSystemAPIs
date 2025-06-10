using System.ComponentModel.DataAnnotations;

namespace School.DTOs.ClassDTOs
{
    public class AddClassDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int GradeLevel { get; set; }
    }
}
