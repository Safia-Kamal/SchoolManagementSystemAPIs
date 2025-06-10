using School.Models;

namespace School.DTOs.AttendanceDTO
{
    public class displayAttendanceDTO
    {
        public int Id { get; set; } 
        public int StudentId { get; set; } 
        public string StudentName {  get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
