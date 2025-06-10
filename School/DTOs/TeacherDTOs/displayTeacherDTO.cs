using System.ComponentModel.DataAnnotations;

namespace School.DTOs.TeacherDTO
{
    public class displayTeacherDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(14)]
        public string NationalId { get; set; }
    }
}
