using School.Models;

namespace School.DTOs.StudentDTOs
{
    public class displayStudentDTO
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public string NationalId { get; set; }
    }
}
