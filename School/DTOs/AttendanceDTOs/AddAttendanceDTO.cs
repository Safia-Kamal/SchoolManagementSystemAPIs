using School.Models;

namespace School.DTOs.AttendanceDTOs
{
    public class AddAttendanceDTO
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public AttendanceStatus Status { get; set; }
    }
}
