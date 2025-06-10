using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public DateTime Date { get; set; }= DateTime.Now;
        public AttendanceStatus Status { get; set; }  
    }
    public enum AttendanceStatus
    {
        Absent = 0,
        Present =1,
        Excused=2
    }

}
