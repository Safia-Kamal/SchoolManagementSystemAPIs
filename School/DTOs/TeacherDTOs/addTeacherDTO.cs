using System.ComponentModel.DataAnnotations;

namespace School.DTOs.TeacherDTOs
{
    public class addTeacherDTO
    {
        public string Name { get; set; }

        [StringLength(14)]
        public string NationalId { get; set; }
    }
}
