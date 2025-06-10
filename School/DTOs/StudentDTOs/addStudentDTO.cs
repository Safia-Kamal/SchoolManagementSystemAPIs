using School.Models;

namespace School.DTOs.StudentDTOs
{
    public class addStudentDTO
    {
        public string Name { get; set; }
        public int ClassId { get; set; }
        public string NationalId { get; set; }
        public int? ParentId { get; set; }


    }
}
