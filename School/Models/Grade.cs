using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public decimal Value { get; set; }
    }
}
