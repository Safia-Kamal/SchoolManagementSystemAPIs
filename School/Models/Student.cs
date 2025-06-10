using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;

namespace School.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public string NationalId { get; set; }

        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
