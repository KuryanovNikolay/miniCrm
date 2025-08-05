using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.SubscriptionsDtos;

public class CreateSubscriptionDto
{
    [Required]
    public Guid TeacherId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public Guid SubjectId { get; set; }

    [Required]
    [Range(1, 100)]
    public int LessonsTotal { get; set; }

    [Required]
    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    public DateTime? StartDate { get; set; }
}
