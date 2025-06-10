using School.Models;

namespace School.DTOs.GradeDTOs
{
    public class displayGradeDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }

        public string SubjectName { get; set; }

        public float Value { get; set; }

    }
}
