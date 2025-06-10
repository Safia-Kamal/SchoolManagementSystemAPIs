using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class TeacherClassSubject
    {
        [Key]
        public int Id { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
