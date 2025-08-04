using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.HomeworkDtos;

public class CreateHomeworkDto
{
    [Required]
    public Guid LessonId { get; set; }

    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? Deadline { get; set; }
}