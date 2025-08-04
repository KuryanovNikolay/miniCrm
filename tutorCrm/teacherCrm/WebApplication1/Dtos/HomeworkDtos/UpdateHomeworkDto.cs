using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.HomeworkDtos;

public class UpdateHomeworkDto
{
    [StringLength(200)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }
}