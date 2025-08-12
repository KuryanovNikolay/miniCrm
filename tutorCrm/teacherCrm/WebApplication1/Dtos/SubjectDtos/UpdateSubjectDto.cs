using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SubjectDtos
{
    public class UpdateSubjectDto
    {
        [Required]
        public Guid TeacherId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}