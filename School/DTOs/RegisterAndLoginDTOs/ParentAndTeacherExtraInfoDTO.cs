using System.ComponentModel.DataAnnotations;

namespace School.DTOs.RegisterAndLoginDTOs
{
    public class ParentAndTeacherExtraInfoDTO
    {
        [Required]
        [StringLength(14)]
        public string NationalId { get; set; }
    }
}
