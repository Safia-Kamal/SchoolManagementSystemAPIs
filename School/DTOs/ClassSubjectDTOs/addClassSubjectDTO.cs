using School.Models;

namespace School.DTOs.ClassSubjectDTOs
{
    public class addClassSubjectDTO
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
    }
}
