using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class ClassSubject
    {
        [Key]
        public int Id { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
