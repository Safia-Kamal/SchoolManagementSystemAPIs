using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ClassSubject> ClassSubjects { get; set; }
        public ICollection<TeacherClassSubject> TeacherClassSubjects { get; set; }
       
    }
}
