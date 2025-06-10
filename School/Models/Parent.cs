using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace School.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        [Required]
        [StringLength(14)]
        public string NationalId { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
