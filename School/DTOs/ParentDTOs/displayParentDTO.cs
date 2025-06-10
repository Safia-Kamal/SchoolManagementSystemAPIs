using System.ComponentModel.DataAnnotations;

namespace School.DTOs.ParentDTOs
{
    public class displayParentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [StringLength(14)]
        public string NationalId { get; set; }
    }
}
