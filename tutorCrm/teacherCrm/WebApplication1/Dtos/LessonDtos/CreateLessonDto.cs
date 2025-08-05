using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.LessonDtos;

public class CreateLessonDto
{
    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public Guid SubjectId { get; set; }

    [Required]
    public DateTime LessonDate { get; set; }

    [Required]
    [Range(1, 480)]
    public int DurationMinutes { get; set; }

    [StringLength(200)]
    public string Topic { get; set; }

    [Range(0, 999999.99)]
    public decimal Price { get; set; }

    public string Notes { get; set; }
}