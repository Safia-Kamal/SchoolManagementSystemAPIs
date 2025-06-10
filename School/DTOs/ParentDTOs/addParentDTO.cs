using System.ComponentModel.DataAnnotations;

namespace School.DTOs.ParentDTOs
{
    public class addParentDTO
    {
        public string Name { get; set; }

        [Required]
        [StringLength(14)]
        public string NationalId { get; set; }
    }
}
