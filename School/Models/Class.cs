using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace School.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int GradeLevel { get; set; }

        public ICollection<Student> Students { get; set; }= new List<Student>();
        public ICollection<Timetable> Timetables { get; set; }= new List<Timetable>();
        public ICollection<ClassSubject> ClassSubjects { get; set; }=new List<ClassSubject>();
        public ICollection<TeacherClassSubject> TeacherClassSubjects { get; set; } = new List<TeacherClassSubject>();

    }
}
