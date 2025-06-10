using School.Models;

namespace School.DTOs.TimetableDTOs
{
    public class displayTimetableDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

        public string SubjectName { get; set; }

        public string TeacherName { get; set; }

        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
