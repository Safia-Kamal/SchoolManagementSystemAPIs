using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School.DTOs.RegisterAndLoginDTOs
{
    public class StudentExtraInfoDTO
    {
        [Required]
        public int ClassId {  get; set; }
        [Required]
        [StringLength(14)]
        public string ParentNationalId { get; set; }
    }
}
