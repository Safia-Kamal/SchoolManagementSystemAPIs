using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(14)]
        public string NationalId { get; set; }
        public ICollection<TeacherClassSubject> TeacherClassSubjects { get; set; }= new List<TeacherClassSubject>();
        

    }
}
