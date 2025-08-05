using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.LessonDtos;

public class UpdateLessonDto
{
    public DateTime? LessonDate { get; set; }

    [Range(1, 480)]
    public int? DurationMinutes { get; set; }

    [StringLength(200)]
    public string Topic { get; set; }

    [StringLength(20)]
    public string Status { get; set; }

    [Range(0, 999999.99)]
    public decimal? Price { get; set; }

    public string Notes { get; set; }
}