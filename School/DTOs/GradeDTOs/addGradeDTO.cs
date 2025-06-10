using School.Models;

namespace School.DTOs.GradeDTOs
{
    public class addGradeDTO
    {
        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public float Value { get; set; }
    }
}
