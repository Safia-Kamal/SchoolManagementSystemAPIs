using School.Models;

namespace School.DTOs.TimetableDTOs
{
    public class addTimetableDTO
    {
        public int ClassId{ get; set; }

        public int SubjectId { get; set; }

        public int TeacherId { get; set; }

        public string DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
